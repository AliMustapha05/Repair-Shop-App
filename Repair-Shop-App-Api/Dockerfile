FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Repair-Shop-App-Api.sln ./
COPY Repair-Shop-App-Api/Repair-Shop-App-Api.csproj Repair-Shop-App-Api/

RUN dotnet restore

COPY . .

WORKDIR /src/Repair-Shop-App-Api
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

EXPOSE 8080

CMD ["dotnet", "Repair-Shop-App-Api.dll"]