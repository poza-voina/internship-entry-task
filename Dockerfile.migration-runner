### build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY . ./

RUN dotnet restore "src/MigrationRunner/MigrationRunner.csproj"
RUN dotnet build "src/MigrationRunner/MigrationRunner.csproj" -c Release

### publish
FROM build AS publish
RUN dotnet publish "src/MigrationRunner/MigrationRunner.csproj" -c Release -o /app/out/MigrationRunner

### runtime
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS runtime
WORKDIR /app

COPY --from=publish /app/out/MigrationRunner ./

ENTRYPOINT ["dotnet", "MigrationRunner.dll"]