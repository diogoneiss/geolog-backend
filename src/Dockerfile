#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["GeoLogBackend.csproj", "."]
RUN dotnet restore "./GeoLogBackend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "GeoLogBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeoLogBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#Preciso usar o CMD para funcionar no heroku
#ENTRYPOINT ["dotnet", "GeoLogBackend.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet GeoLogBackend.dll