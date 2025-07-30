# Multi-stage build
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7001
EXPOSE 7002

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy nuget.config first
COPY ["nuget.config", "./"]

# Copy project files
COPY ["src/Domain/MinimalAirbnb.Domain.csproj", "src/Domain/"]
COPY ["src/Application/MinimalAirbnb.Application.csproj", "src/Application/"]
COPY ["src/Infrastructure/MinimalAirbnb.Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/API/MinimalAirbnb.API.csproj", "src/API/"]

# Restore dependencies
RUN dotnet restore "src/API/MinimalAirbnb.API.csproj"

# Copy source code
COPY . .

# Build the application
WORKDIR "/src/src/API"
RUN dotnet build "MinimalAirbnb.API.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "MinimalAirbnb.API.csproj" -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create non-root user
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

ENTRYPOINT ["dotnet", "MinimalAirbnb.API.dll"] 