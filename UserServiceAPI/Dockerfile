# Использовать официальный образ SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копировать файлы проекта и восстановить зависимости
COPY UserServiceAPI/UserServiceAPI.csproj ./UserServiceAPI/
RUN dotnet restore ./UserServiceAPI/UserServiceAPI.csproj

# Скопировать все остальные файлы проекта
COPY . .

# Сборка проекта
WORKDIR /src/UserServiceAPI
RUN dotnet build UserServiceAPI.csproj -c $BUILD_CONFIGURATION -o /app/build

# Публикация проекта
RUN dotnet publish UserServiceAPI.csproj -c $BUILD_CONFIGURATION -o /app/publish

# Использовать образ ASP.NET для выполнения приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Копировать опубликованные файлы в образ для выполнения
COPY --from=build /app/publish .

# Настроить команду для запуска приложения
ENTRYPOINT ["dotnet", "UserServiceAPI.dll"]
