# Build stage (uses SDK image)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

#1. Copy csproj and restore as distinct layers
COPY ["BlogService/BlogService.csproj", "BlogService/"]
RUN dotnet restore "BlogService/BlogService.csproj"

#2. Copy everything else
COPY . .

#3. Build and publish
WORKDIR "/src/BlogService"
RUN dotnet publish "BlogService.csproj" -c Release -o /app/publish

# Runtime stage (uses ASP.NET image)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BlogService.dll"]