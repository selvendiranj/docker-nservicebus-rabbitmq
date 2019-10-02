FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Receiver/Receiver.csproj", "Receiver/"]
COPY ["Messages/Messages.csproj", "Messages/"]
RUN dotnet restore "Receiver/Receiver.csproj"
COPY . .
WORKDIR "/src/Receiver"
RUN dotnet build "Receiver.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Receiver.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Receiver.dll"]