FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["*.sln", "."]
COPY ["useManagementAPI/useManagementAPI.csproj", "useManagementAPI/"]
RUN dotnet restore "./useManagementAPI/useManagementAPI.csproj"
COPY . .
WORKDIR "/src/useManagementAPI"
RUN dotnet build "useManagementAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "useManagementAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "useManagementAPI.dll"]
