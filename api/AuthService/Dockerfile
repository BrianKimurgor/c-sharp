# Build stage with .NET 9.0 SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1. Copy ONLY project files first for efficient caching
COPY ["AuthService/AuthService.csproj", "AuthService/"]
RUN dotnet restore "AuthService/AuthService.csproj"

# 2. Copy remaining source code
COPY . .

# 3. Build and publish
WORKDIR "/src/AuthService"
RUN dotnet publish -c Release -o /app/publish

# Runtime stage with .NET 9.0 ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# 4. Copy published output
COPY --from=build /app/publish .

# 5. Entry point
ENTRYPOINT ["dotnet", "AuthService.dll"]