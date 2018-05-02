FROM microsoft/dotnet:2-sdk as build
WORKDIR /docker
COPY ./src .
ENTRYPOINT [ "dotnet" ]
