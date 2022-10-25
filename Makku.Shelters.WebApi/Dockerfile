#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Makku.Shelters.WebApi/Makku.Shelters.WebApi.csproj", "Makku.Shelters.WebApi/"]
RUN dotnet restore "Makku.Shelters.WebApi/Makku.Shelters.WebApi.csproj"
COPY . .
WORKDIR "/src/Makku.Shelters.WebApi"
RUN dotnet build "Makku.Shelters.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Makku.Shelters.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Makku.Shelters.WebApi.dll"]