#!/bin/bash
set -e
echo "Waiting for Postgres to be ready..."

until pg_isready -h db -p 5432 -U postgres; do
  >&2 echo "Postgres is unavailable - sleeping"
  sleep 3
done

echo "Postgres is up - continuing..."
echo "Applying EF migrations..."
dotnet ef database update \
  --project /src/AuthService/AuthService.csproj \
  --startup-project /src/AuthService

echo "Starting the app..."
exec dotnet /app/AuthService.dll