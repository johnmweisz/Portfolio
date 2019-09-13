FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["Portfolio.csproj", "./"]
RUN dotnet restore "./Portfolio.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Portfolio.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Portfolio.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portfolio.dll"]
