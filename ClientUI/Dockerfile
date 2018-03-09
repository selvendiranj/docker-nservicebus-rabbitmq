# Build runtime image
FROM microsoft/dotnet:runtime
WORKDIR /ClientUI
COPY ./bin/Debug/netcoreapp2.0/publish .
ENTRYPOINT ["dotnet", "ClientUI.dll"]