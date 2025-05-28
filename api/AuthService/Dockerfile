# ---------- Build Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy only the .csproj file to restore dependencies
COPY AuthService/AuthService.csproj ./AuthService/
RUN dotnet restore ./AuthService/AuthService.csproj

# Copy the rest of the source code
COPY . .

# Optional: Change to the AuthService directory if needed
WORKDIR /src/AuthService

# Install EF tool globally (needed only if you’re using EF commands during build)
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Publish the application
RUN dotnet publish AuthService.csproj -c Release -o /app/publish

# ---------- Final Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /app

# Install PostgreSQL client (if needed by entrypoint or runtime)
RUN apt-get update && apt-get install -y postgresql-client && rm -rf /var/lib/apt/lists/*

# Copy the published app and required tools
COPY --from=build /app/publish .
COPY --from=build /root/.dotnet /root/.dotnet
COPY --from=build /root/.nuget /root/.nuget
ENV PATH="$PATH:/root/.dotnet/tools"

# (Optional) Copy source code if required at runtime (e.g., migrations, hot reload)
COPY --from=build /src /src

# Copy and prepare the entrypoint script
COPY ./AuthService/entrypoint.sh .
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]
