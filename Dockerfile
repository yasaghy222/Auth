FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5280

ENV ASPNETCORE_URLS=http://+:5280 
    # DB_HOST=sql \
    # DB_PORT=1433 \
    # DB_NAME=AuthenticateDB \
    # DB_SA_PASSWORD=Admin@123 \
    # ASPNETCORE_ENVIRONMENT=Development 

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Authenticate.csproj", "./"]
RUN dotnet restore "Authenticate.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Authenticate.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Authenticate.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authenticate.dll"]
