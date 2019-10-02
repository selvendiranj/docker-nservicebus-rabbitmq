FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Sender/Sender.csproj", "Sender/"]
COPY ["Messages/Messages.csproj", "Messages/"]
RUN dotnet restore "Sender/Sender.csproj"
COPY . .
WORKDIR "/src/Sender"
RUN dotnet build "Sender.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sender.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sender.dll"]