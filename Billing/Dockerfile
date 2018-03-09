# Build runtime image
FROM microsoft/dotnet:runtime
WORKDIR /Billing
COPY ./bin/Debug/netcoreapp2.0/publish .
ENTRYPOINT ["dotnet", "Billing.dll"]