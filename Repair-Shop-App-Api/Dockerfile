FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy everything
COPY . ./

# Go into project folder
WORKDIR /app/Repair-Shop-App-Api

# Restore + publish
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/Repair-Shop-App-Api/out .

CMD ["dotnet", "Repair-Shop-App-Api.dll"]