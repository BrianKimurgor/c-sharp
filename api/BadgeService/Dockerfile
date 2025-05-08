# Build Stage (uses SDK image)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["BadgeService/BadgeService.csproj", "BadgeService/"]
RUN dotnet restore "BadgeService/BadgeService.csproj"

COPY . .
WORKDIR "/src/BadgeService"
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet publish "BadgeService.csproj" -c Release -o /app/publish

# Runtime stage (uses ASP.NET image)
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
COPY BadgeService/entrypoint.sh .
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]
