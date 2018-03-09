docker-compose down
dotnet build
dotnet publish
docker-compose build
docker-compose up -d