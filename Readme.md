# Cross application Messaging using docker containers

## Prerequisites
* Docker for Windows
* Visual Studio 2017 or Visual Studio Code
* .NET Core SDK

After installing Docker for windows switch to Linux containers. This is needed because the RabbitMQ docker image is not readily available for windows containers (RabbitMQ for windows containers needs manual installation)

## Nuget Package dependencies
* NServiceBus
* NServiceBus.RabbitMQ

## Build & generate dotnet binaries
* cd `<project-root>`
* `dotnet restore`
* `dotnet build`
* `dotnet publish`

## Create and run containers
* docker-compose build
* docker-compose up -d

## Connect to docker container logs
* docker-compose logs sender
* docker-compose logs receiver

## Sample Docker Image file

```docker
# Build runtime image
FROM microsoft/dotnet:runtime
WORKDIR /ClientUI
COPY ./bin/Debug/netcoreapp2.0/publish .
ENTRYPOINT ["dotnet", "ClientUI.dll"]
```

## Docker Compsoe file

```docker
version: "3"
services:
    clientui:
        image: clientui
        build:
            context: ./ClientUI/
            dockerfile: Dockerfile
        depends_on:
            - sales
            - rabbitmq
        command: ["CMD-SHELL", "if rabbitmqctl status; then \nexit 0 \nfi \nexit 1"]
    sales:
        image: sales
        build:
            context: ./Sales/
            dockerfile: Dockerfile
        depends_on:
            - billing
            - shipping
            - rabbitmq
        command: ["CMD-SHELL", "if rabbitmqctl status; then \nexit 0 \nfi \nexit 1"]
    billing:
        image: billing
        build:
            context: ./Billing/
            dockerfile: Dockerfile
        depends_on:
            - shipping
            - rabbitmq
        command: ["CMD-SHELL", "if rabbitmqctl status; then \nexit 0 \nfi \nexit 1"]
    shipping:
        image: shipping
        build:
            context: ./Shipping/
            dockerfile: Dockerfile
        depends_on:
            - rabbitmq
        command: ["CMD-SHELL", "if rabbitmqctl status; then \nexit 0 \nfi \nexit 1"]
    rabbitmq:
        image: "rabbitmq:3-management"
        ports:
            - "15672:15672"
        healthcheck:
            test: ["CMD-SHELL", "if rabbitmqctl status; then \nexit 0 \nfi \nexit 1"]
            interval: 10s
            retries: 5
```
