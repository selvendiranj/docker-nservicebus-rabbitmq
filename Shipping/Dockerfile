# Build runtime image
FROM microsoft/dotnet:runtime
WORKDIR /Shipping
COPY ./bin/Debug/netcoreapp2.0/publish .
ENTRYPOINT ["dotnet", "Shipping.dll"]