# ---------- Build Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy only the csproj file to cache dependencies
COPY WorkService/WorkService.csproj ./WorkService/
RUN dotnet restore ./WorkService/WorkService.csproj

# Copy all files
COPY . .

# Set the working directory to the service folder
WORKDIR /src/WorkService

# Optional: install EF CLI globally (if needed)
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Publish the application
RUN dotnet publish WorkService.csproj -c Release -o /app/publish

# ---------- Runtime Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /app

# Install PostgreSQL client (e.g. for readiness check or migrations)
RUN apt-get update && apt-get install -y postgresql-client && rm -rf /var/lib/apt/lists/*

# Copy published app and tools
COPY --from=build /app/publish .
COPY --from=build /root/.dotnet /root/.dotnet
COPY --from=build /root/.nuget /root/.nuget
ENV PATH="$PATH:/root/.dotnet/tools"

# Optional: Copy source code if needed at runtime
COPY --from=build /src /src

# Copy and prepare entrypoint
COPY ./WorkService/entrypoint.sh .
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]
