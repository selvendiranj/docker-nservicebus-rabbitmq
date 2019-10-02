FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Client.GUI/Client.GUI.csproj", "Client.GUI/"]
COPY ["Messages/Messages.csproj", "Messages/"]
RUN dotnet restore "Client.GUI/Client.GUI.csproj"
COPY . .
WORKDIR "/src/Client.GUI"
RUN dotnet build "Client.GUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Client.GUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Client.GUI.dll"]