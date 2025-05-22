# Build stage with .NET 9.0 SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["/AuthService.csproj", "AuthService/"]
RUN dotnet restore "/AuthService.csproj"

COPY . .
WORKDIR "/src/AuthService"
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet publish "AuthService.csproj" -c Release -o /app/publish

# Runtime stage with .NET 9.0 ASP.NET runtime
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /app

#Install PostgreSQL client for pg_isready
RUN apt-get update && apt-get install -y postgresql-client && rm -rf /var/lib/apt/lists/*

# 4. Copy published output
COPY --from=build /app/publish .
COPY --from=build /root/.dotnet /root/.dotnet
COPY --from=build /root/.nuget /root/.nuget
ENV PATH="$PATH:/root/.dotnet/tools"


COPY --from=build /src /src

# Copy the entrypoint script
COPY /entrypoint.sh .
RUN chmod +x ./entrypoint.sh

# 5. Entry point
ENTRYPOINT ["./entrypoint.sh"]
