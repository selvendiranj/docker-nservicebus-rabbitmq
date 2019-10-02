FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Billing/Billing.csproj", "Billing/"]
COPY ["Messages/Messages.csproj", "Messages/"]
RUN dotnet restore "Billing/Billing.csproj"
COPY . .
WORKDIR "/src/Billing"
RUN dotnet build "Billing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Billing.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Billing.dll"]