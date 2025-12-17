FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY EmployeeManager.API.sln ./

COPY EmployeeManager.API/*.csproj EmployeeManager.API/
COPY EmployeeManager.Application/*.csproj EmployeeManager.Application/
COPY EmployeeManager.Domain/*.csproj EmployeeManager.Domain/
COPY EmployeeManager.Infrastructure/*.csproj EmployeeManager.Infrastructure/
COPY EmployeeManager.Test/*.csproj EmployeeManager.Test/

RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .
EXPOSE 8080

ENTRYPOINT ["dotnet", "EmployeeManager.API.dll"]
