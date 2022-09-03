#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 80
EXPOSE 5050

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BancoPichincha.API/BancoPichincha.API.csproj", "BancoPichincha.API/"]
COPY ["BancoPichincha.Services/BancoPichincha.Services.csproj", "BancoPichincha.Services/"]
COPY ["BancoPichincha.Repository/BancoPichincha.Repository.csproj", "BancoPichincha.Repository/"]
COPY ["BancoPichincha.Core/BancoPichincha.Core.csproj", "BancoPichincha.Core/"]
RUN dotnet restore "BancoPichincha.API/BancoPichincha.API.csproj"
COPY . .
WORKDIR "/src/BancoPichincha.API"
RUN dotnet build "BancoPichincha.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BancoPichincha.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BancoPichincha.API.dll"]