FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Fix COPY here: copy only the csproj from current context root
COPY ["AuthService.csproj", "./"]
RUN dotnet restore "AuthService.csproj"

COPY . .
WORKDIR "/src"
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet publish "AuthService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /app

RUN apt-get update && apt-get install -y postgresql-client && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .
COPY --from=build /root/.dotnet /root/.dotnet
COPY --from=build /root/.nuget /root/.nuget
ENV PATH="$PATH:/root/.dotnet/tools"

COPY --from=build /src /src

COPY ./entrypoint.sh .
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]
