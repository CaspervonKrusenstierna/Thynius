# Step 1: Build the React app
FROM node:20.12.2 AS build

# Set the working directory to the React client folder
WORKDIR /app/thyniusweb.client/

# Copy the React app files
COPY thyniusweb.client/package*.json . 
RUN npm install

# Copy the rest of the React client code
COPY thyniusweb.client/ . 
RUN ls -alh
RUN npm run build

# Step 2: Build the ASP.NET Core backend
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-backend

# Set the working directory to the ASP.NET Core backend folder
WORKDIR /app/ThyniusWeb.Server/

# Copy only the .csproj file first for dependency restore
COPY ThyniusWeb.Server/ThyniusWeb.Server.csproj .

# Restore dependencies
RUN dotnet restore ThyniusWeb.Server.csproj

# Copy the rest of the backend files
COPY ThyniusWeb.Server/ . 

# Build the backend
RUN dotnet build ThyniusWeb.Server.csproj -c Debug -o /app/build

# Publish the backend
RUN dotnet publish ThyniusWeb.Server.csproj -c Debug -o /app/publish

# Step 3: Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set the working directory for the final image
WORKDIR /app

# Copy the published backend
COPY --from=build-backend /app/publish .

# Copy the React app build output to the ASP.NET Core wwwroot folder
COPY --from=build /app/thyniusweb.client/dist ./wwwroot/

# Expose the port for the app
EXPOSE 80

# Set the entry point to run the backend
ENTRYPOINT ["dotnet", "ThyniusWeb.Server.dll"]