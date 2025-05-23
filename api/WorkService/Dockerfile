# Build stage (uses SDK image)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["WorkService.csproj", "./"]
RUN dotnet restore "WorkService.csproj"

COPY . .
WORKDIR "/src"
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet publish "WorkService.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /app

# ✅ Install PostgreSQL client for pg_isready
RUN apt-get update && apt-get install -y postgresql-client && rm -rf /var/lib/apt/lists/*

# Copy published app and tools
COPY --from=build /app/publish .
COPY --from=build /root/.dotnet /root/.dotnet
COPY --from=build /root/.nuget /root/.nuget
ENV PATH="$PATH:/root/.dotnet/tools"

# Add this to the final stage
COPY --from=build /src /src

# Copy the entrypoint
COPY ./entrypoint.sh .
RUN chmod +x ./entrypoint.sh

ENTRYPOINT [ "./entrypoint.sh" ]