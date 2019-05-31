FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 44392
ENV ASPNETCORE_URLS https://*:44392



FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY WebApp1/WebApp1.csproj WebApp1/
COPY DockerModel/DockerModel.csproj DockerModel/
RUN dotnet restore WebApp1/WebApp1.csproj
COPY . .
WORKDIR /src/WebApp1
RUN dotnet build WebApp1.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebApp1.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApp1.dll"]
