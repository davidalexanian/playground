#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/SonarqubeSampleProject.csproj", "src/"]
RUN dotnet restore "src/SonarqubeSampleProject.csproj"
COPY . .
WORKDIR "/src/src"

RUN apt-get update && apt-get install -y openjdk-17-jdk
RUN dotnet tool install --global dotnet-sonarscanner
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet sonarscanner begin \
	/k:"SonarqubeSampleProject" \
	/d:sonar.host.url="http://host.docker.internal:9000" \
	/d:sonar.token="sqa_da13d32640b966827a776f231d86767a25458b29"

RUN ls
RUN dotnet build "SonarqubeSampleProject.csproj" -c Release -o /app/build

RUN dotnet sonarscanner end /d:sonar.token="sqa_da13d32640b966827a776f231d86767a25458b29"

FROM build AS publish
RUN dotnet publish "SonarqubeSampleProject.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SonarqubeSampleProject.dll"]