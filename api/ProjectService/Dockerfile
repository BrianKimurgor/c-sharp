# ---------- Build Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy only the .csproj file for better layer caching
COPY ProjectService/ProjectService.csproj ./ProjectService/
RUN dotnet restore ./ProjectService/ProjectService.csproj

# Copy all source files
COPY . .

# Set working directory to the service folder
WORKDIR /src/ProjectService

# Install EF Core CLI (optional, only if needed during build)
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Publish the app
RUN dotnet publish ProjectService.csproj -c Release -o /app/publish

# ---------- Runtime Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /app

# Install PostgreSQL client (e.g., for pg_isready or database operations)
RUN apt-get update && apt-get install -y postgresql-client && rm -rf /var/lib/apt/lists/*

# Copy published output from the build stage
COPY --from=build /app/publish .

# Optional: Copy EF Core CLI and tools if needed at runtime
COPY --from=build /root/.dotnet /root/.dotnet
COPY --from=build /root/.nuget /root/.nuget
ENV PATH="$PATH:/root/.dotnet/tools"

# Optional: Copy full source tree (needed only if entrypoint or EF migrations require it)
COPY --from=build /src /src

# Copy and make entrypoint executable
COPY ./ProjectService/entrypoint.sh .
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]
