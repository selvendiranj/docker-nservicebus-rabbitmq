FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Shipping/Shipping.csproj", "Shipping/"]
COPY ["Messages/Messages.csproj", "Messages/"]
RUN dotnet restore "Shipping/Shipping.csproj"
COPY . .
WORKDIR "/src/Shipping"
RUN dotnet build "Shipping.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shipping.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shipping.dll"]