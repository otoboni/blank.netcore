# Build stage
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

COPY *.sln .
COPY web/*.csproj ./web/
RUN dotnet restore

COPY . .
RUN dotnet publish web -c Release -o /app/publish --no-restore

# Runtime stage
FROM --platform=$TARGETPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "web.dll"]
