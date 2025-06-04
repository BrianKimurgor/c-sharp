# ---------- Build Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy only the .csproj file for optimized layer caching
COPY ImageUploadService/ImageUploadService.csproj ./ImageUploadService/
RUN dotnet restore ./ImageUploadService/ImageUploadService.csproj

# Copy the full source after restore
COPY . .

# Change working directory to the EducationService folder
WORKDIR /src/ImageUploadService

# Install EF Core CLI if needed for build (migrations, etc.)
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Publish the application
RUN dotnet publish ImageUploadService.csproj -c Release -o /app/publish

# ---------- Runtime Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /app

# Install PostgreSQL client (for runtime tools like `pg_isready`)
RUN apt-get update && apt-get install -y postgresql-client && rm -rf /var/lib/apt/lists/*

# Copy the published output from build
COPY --from=build /app/publish .

# Optional: Copy tools if you need them during container runtime
COPY --from=build /root/.dotnet /root/.dotnet
COPY --from=build /root/.nuget /root/.nuget
ENV PATH="$PATH:/root/.dotnet/tools"

# Optional: Copy source files, only if needed by entrypoint/migrations at runtime
COPY --from=build /src /src

# Copy entrypoint script and make it executable
COPY ./ImageUploadService/entrypoint.sh .
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]