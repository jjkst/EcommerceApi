FROM ubuntu:24.10 AS base

# Install dependencies and .NET Core runtime
RUN apt-get update && apt-get install -y wget \
    && wget https://packages.microsoft.com/config/ubuntu/24.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && apt-get update && apt-get install -y aspnetcore-runtime-8.0 \
    && rm packages-microsoft-prod.deb

WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EcommerceApi.csproj", "."]
RUN dotnet restore "./EcommerceApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "EcommerceApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcommerceApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose the port the application will run on
EXPOSE 5002

# Set environment variables for better performance in production
ENV DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true \
    DOTNET_URLS=http://+:5002

ENTRYPOINT ["dotnet", "EcommerceApi.dll"]
