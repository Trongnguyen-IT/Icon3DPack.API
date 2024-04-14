#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Icon3DPack.API.Host/Icon3DPack.API.Host.csproj", "src/Icon3DPack.API.Host/"]
COPY ["src/Icon3DPack.API.Application/Icon3DPack.API.Application.csproj", "src/Icon3DPack.API.Application/"]
COPY ["src/Icon3DPack.API.AwsS3/Icon3DPack.API.AwsS3.csproj", "src/Icon3DPack.API.AwsS3/"]
COPY ["src/Icon3DPack.API.DataAccess/Icon3DPack.API.DataAccess.csproj", "src/Icon3DPack.API.DataAccess/"]
COPY ["src/Icon3DPack.API.Core/Icon3DPack.API.Core.csproj", "src/Icon3DPack.API.Core/"]
COPY ["src/Icon3DPack.API.Shared/Icon3DPack.API.Shared.csproj", "src/Icon3DPack.API.Shared/"]
RUN dotnet restore "./src/Icon3DPack.API.Host/Icon3DPack.API.Host.csproj"
COPY . .
WORKDIR "/src/src/Icon3DPack.API.Host"
RUN dotnet build "./Icon3DPack.API.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Icon3DPack.API.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Icon3DPack.API.Host.dll"]