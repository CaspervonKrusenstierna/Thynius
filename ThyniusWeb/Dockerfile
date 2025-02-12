
FROM node:20.12.2 AS build

WORKDIR /app/thyniusweb.client/

COPY thyniusweb.client/package*.json . 
RUN npm install

COPY thyniusweb.client/ . 
RUN ls -alh
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-backend

WORKDIR /app/ThyniusWeb.Server/

COPY ThyniusWeb.Server/ThyniusWeb.Server.csproj .

RUN dotnet restore ThyniusWeb.Server.csproj

COPY ThyniusWeb.Server/ . 

RUN dotnet build ThyniusWeb.Server.csproj -c Debug -o /app/build

RUN dotnet publish ThyniusWeb.Server.csproj -c Debug -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build-backend /app/publish .

COPY --from=build /app/thyniusweb.client/dist ./wwwroot/

EXPOSE 80

ENTRYPOINT ["dotnet", "ThyniusWeb.Server.dll"]
