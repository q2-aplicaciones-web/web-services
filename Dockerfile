# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY . ./
RUN dotnet restore Q2.Web-Service.API/Q2.Web-Service.API.csproj
RUN dotnet publish Q2.Web-Service.API/Q2.Web-Service.API.csproj -c Release -o out

# Use the official .NET runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
# Copy .env and appsettings files if needed
# COPY Q2.Web-Service.API/.env ./
COPY Q2.Web-Service.API/appsettings.*json ./
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 5000
ENTRYPOINT ["dotnet", "Q2.Web-Service.API.dll"]
