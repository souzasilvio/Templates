FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS http://*:5000

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["WebApi1/WebApi1.csproj", "WebApi1/"]
COPY ["DockerModel/DockerModel.csproj", "DockerModel/"]
RUN dotnet restore "WebApi1/WebApi1.csproj"
COPY . .
WORKDIR "/src/WebApi1"
RUN dotnet build "WebApi1.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebApi1.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApi1.dll"]