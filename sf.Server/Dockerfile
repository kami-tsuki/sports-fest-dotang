# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Install Node.js v18.x
RUN curl -fsSL https://deb.nodesource.com/setup_18.x | bash - \
    && apt-get install -y nodejs

# Install Angular CLI
RUN npm install -g @angular/cli

# Copy project files
COPY sf.Server/ sf.Server/
COPY sf.client/ sf.client/
RUN dotnet restore sf.Server/sf.Server.csproj

# Copy the rest of the files and build the project
COPY . .
WORKDIR /src/sf.Server
RUN dotnet build sf.Server.csproj -c $BUILD_CONFIGURATION -o /app/build

# Build Angular app
WORKDIR /src/sf.client
RUN npm install
RUN ng build --configuration production --output-path=/app/build/wwwroot

# Publish image for publishing the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/sf.Server
RUN dotnet publish sf.Server.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image for running the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /app/build/wwwroot ./wwwroot
COPY sf.Server/README.md .
COPY sf.Server/CHANGELOG.md .
#COPY CHANGELOG.md .

# Set the entrypoint and environment variables based on the build configuration
ARG BUILD_CONFIGURATION=Release
ENV ASPNETCORE_ENVIRONMENT=${BUILD_CONFIGURATION}
ENV ASPNETCORE_URLS=http://+:8080

# If in Debug mode, expose port 8081
RUN if [ "$BUILD_CONFIGURATION" = "Debug" ]; then \
    echo "Exposing port 8081 for Debug mode" && \
    export ASPNETCORE_URLS=http://+:8081; \
fi

ENTRYPOINT ["dotnet", "sf.Server.dll"]