FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs
ARG BUILD_CONFIGURATION=Release

COPY ["thyniusweb.client/nuget.config", "thyniusweb.client/"]
COPY ["ThyniusWeb.Server/ThyniusWeb.Server.csproj", "ThyniusWeb.Server/"]
COPY ["thyniusweb.client/thyniusweb.client.esproj", "thyniusweb.client/"]
RUN dotnet restore "./ThyniusWeb.Server/./ThyniusWeb.Server.csproj"
COPY . .
WORKDIR "/ThyniusWeb.Server"
RUN dotnet build "./ThyniusWeb.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ThyniusWeb.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThyniusWeb.Server.dll"]