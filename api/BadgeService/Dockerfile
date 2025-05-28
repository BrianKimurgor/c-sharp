# ---------- Build Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy only the .csproj file first for faster caching and efficient layer reuse
COPY BadgeService/BadgeService.csproj ./BadgeService/
RUN dotnet restore ./BadgeService/BadgeService.csproj

# Now copy the rest of the source code
COPY . .

# Change to the BadgeService directory for publish
WORKDIR /src/BadgeService

# Install EF Core CLI globally if you need it during build
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Publish the application
RUN dotnet publish BadgeService.csproj -c Release -o /app/publish

# ---------- Final Runtime Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /app

# Install PostgreSQL client (e.g., for `pg_isready` or migrations)
RUN apt-get update && apt-get install -y postgresql-client && rm -rf /var/lib/apt/lists/*

# Copy the published output
COPY --from=build /app/publish . 

# Optional: Copy dotnet tools if EF Core CLI or others are needed at runtime
COPY --from=build /root/.dotnet /root/.dotnet
COPY --from=build /root/.nuget /root/.nuget
ENV PATH="$PATH:/root/.dotnet/tools"

# Optional: Copy full source (needed only if runtime uses it, like EF migrations)
COPY --from=build /src /src

# Copy the entrypoint script and make it executable
COPY ./BadgeService/entrypoint.sh .
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]
