# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OfferManager.WebApi/OfferManager.WebApi.csproj", "./OfferManager.WebApi/"]
COPY ["OfferManager.Domain/OfferManager.Domain.csproj", "./OfferManager.Domain/"]
COPY ["OfferManager.Storage/OfferManager.Storage.csproj", "./OfferManager.Storage/"]
COPY ["OfferManager.Services/OfferManager.Services.csproj", "./OfferManager.Services/"]
RUN dotnet restore "OfferManager.WebApi/OfferManager.WebApi.csproj"
COPY . .
WORKDIR "/src/OfferManager.WebApi"
RUN dotnet publish "OfferManager.WebApi.csproj" -c Release -o /app/publish

# Use the official ASP.NET runtime image for the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OfferManager.WebApi.dll"]
