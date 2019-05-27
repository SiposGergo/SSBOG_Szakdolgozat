FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app
COPY . ./

# Restore nuget packages
RUN dotnet restore

# Build app and tests
Run dotnet publish -c Release -o out -r linux-x64

# Run tests when start container
ENTRYPOINT ["dotnet","test"]