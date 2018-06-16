FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY Rocket.Chat.MongoReader/Rocket.Chat.MongoReader.csproj Rocket.Chat.MongoReader/
RUN dotnet restore Rocket.Chat.MongoReader/Rocket.Chat.MongoReader.csproj
COPY . .
WORKDIR /src/Rocket.Chat.MongoReader
RUN dotnet build Rocket.Chat.MongoReader.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Rocket.Chat.MongoReader.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Rocket.Chat.MongoReader.dll"]
