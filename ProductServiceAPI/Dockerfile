# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем только файлы csproj для восстановления зависимостей
COPY "ProductServiceAPI/ProductServiceAPI.csproj" "ProductServiceAPI/"
COPY "UserServiceAPI/UserServiceAPI.csproj" "UserServiceAPI/"

# Восстановление зависимостей для обоих сервисов
RUN dotnet restore "./ProductServiceAPI/ProductServiceAPI.csproj"
RUN dotnet restore "./UserServiceAPI/UserServiceAPI.csproj"

# Скопировать остальные файлы проекта
COPY . .

# Собираем продукт
WORKDIR "/src/ProductServiceAPI"
RUN dotnet build "./ProductServiceAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build
