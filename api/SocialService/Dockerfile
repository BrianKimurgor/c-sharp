# ---------- Build Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy only the .csproj file to enable layer caching
COPY SocialService/SocialService.csproj ./SocialService/
RUN dotnet restore ./SocialService/SocialService.csproj

# Copy all source files
COPY . .

# Set the working directory to the service folder
WORKDIR /src/SocialService

# Optional: Install EF Core CLI (only if needed during build)
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Publish the app
RUN dotnet publish SocialService.csproj -c Release -o /app/publish

# ---------- Runtime Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /app

# Install PostgreSQL client (e.g., for pg_isready or migrations)
RUN apt-get update && apt-get install -y postgresql-client && rm -rf /var/lib/apt/lists/*

# Copy published app and optional tooling
COPY --from=build /app/publish .
COPY --from=build /root/.dotnet /root/.dotnet
COPY --from=build /root/.nuget /root/.nuget
ENV PATH="$PATH:/root/.dotnet/tools"

# Optional: Copy source tree if needed at runtime
COPY --from=build /src /src

# Copy and make entrypoint executable
COPY ./SocialService/entrypoint.sh .
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]
