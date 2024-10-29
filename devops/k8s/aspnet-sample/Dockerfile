FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["aspnet-sample.csproj", "./"]
RUN dotnet restore "aspnet-sample.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "aspnet-sample.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "aspnet-sample.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
LABEL app="aspnet-sample-mvc"
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "aspnet-sample.dll"]
