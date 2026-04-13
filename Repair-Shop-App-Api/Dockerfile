FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy solution + project files first (important for restore)
COPY Repair-Shop-App-Api.sln ./
COPY Repair-Shop-App-Api/Repair-Shop-App-Api.csproj Repair-Shop-App-Api/

# Restore dependencies
RUN dotnet restore

# Copy everything else
COPY . .

# Build & publish
WORKDIR /src/Repair-Shop-App-Api
RUN dotnet publish -c Release -o /app/out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 8080

CMD ["dotnet", "Repair-Shop-App-Api.dll"]