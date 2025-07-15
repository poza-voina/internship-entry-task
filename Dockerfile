### build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore "src/InternshipEntryTask.Api/InternshipEntryTask.Api.csproj"
RUN dotnet build "src/InternshipEntryTask.Api/InternshipEntryTask.Api.csproj" -c Release

### publish
FROM build AS publish
RUN dotnet publish "src/InternshipEntryTask.Api/InternshipEntryTask.Api.csproj" -c Release -o /app/out/InternshipEntryTask.Api

### runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=publish /app/out/InternshipEntryTask.Api .

EXPOSE 8080

ENTRYPOINT ["dotnet", "InternshipEntryTask.Api.dll"]