#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Delivery-App-Code-Challenge/Delivery-App-Code-Challenge.csproj", "Delivery-App-Code-Challenge/"]
RUN dotnet restore "Delivery-App-Code-Challenge/Delivery-App-Code-Challenge.csproj"
COPY . .
WORKDIR "/src/Delivery-App-Code-Challenge"
RUN dotnet build "Delivery-App-Code-Challenge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Delivery-App-Code-Challenge.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Delivery-App-Code-Challenge.dll"]