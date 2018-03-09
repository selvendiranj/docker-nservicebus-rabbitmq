# Build runtime image
FROM microsoft/dotnet:runtime
WORKDIR /Sales
COPY ./bin/Debug/netcoreapp2.0/publish .
ENTRYPOINT ["dotnet", "Sales.dll"]