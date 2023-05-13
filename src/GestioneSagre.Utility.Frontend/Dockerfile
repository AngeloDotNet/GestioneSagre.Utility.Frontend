FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base

ENV DEBIAN_FRONTEND=noninteractive
RUN apt-get update
RUN apt-get -y install tzdata
RUN ln -fs /usr/share/zoneinfo/Europe/Rome /etc/localtime
RUN dpkg-reconfigure --frontend noninteractive tzdata

WORKDIR /webapp

FROM base as deps
COPY . .
RUN dotnet restore

FROM deps as build
COPY . .
RUN dotnet publish -c Release -o compiled

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as deploy
ENV ASPNETCORE_URLS=http://*:5001
ENV ASPNETCORE_ENVIRONMENT="production"
COPY --from=build /webapp/compiled .

EXPOSE 5001
ENTRYPOINT ["dotnet", "GestioneSagre.Utility.Frontend.dll"]