FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Platform/Platform.csproj", "Platform/"]
RUN dotnet restore "Platform/Platform.csproj"
COPY . .
WORKDIR "/src/Platform"
RUN dotnet build "Platform.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Platform.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Platform.dll"]