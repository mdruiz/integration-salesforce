FROM microsoft/dotnet:2.0-sdk as build
WORKDIR /docker
COPY ./src .
RUN dotnet build Integration.Salesforce.sln

FROM microsoft/aspnetcore:2.0 as deploy
WORKDIR /webapi
COPY --from=build /docker/Integration.Salesforce.Service/bin/Debug/netcoreapp2.0/ .
ENV ASPNETCORE_URLS=http://+:80/
EXPOSE 80
CMD [ "dotnet", "Integration.Salesforce.Service.dll" ]
