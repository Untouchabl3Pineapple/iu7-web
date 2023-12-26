# Используем официальный образ ASP.NET Core в качестве базового образа
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

# Используем официальный образ SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["db_cp.csproj", "."]
RUN dotnet restore "./db_cp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "db_cp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "db_cp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "db_cp.dll"]
