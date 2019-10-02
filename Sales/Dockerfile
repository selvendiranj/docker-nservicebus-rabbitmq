FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Sales/Sales.csproj", "Sales/"]
COPY ["Messages/Messages.csproj", "Messages/"]
RUN dotnet restore "Sales/Sales.csproj"
COPY . .
WORKDIR "/src/Sales"
RUN dotnet build "Sales.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sales.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sales.dll"]