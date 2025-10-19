FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src

COPY ["Orcamentaria.ConfigBag.API/Orcamentaria.ConfigBag.API.csproj", "Orcamentaria.ConfigBag.API/"]
COPY ["Orcamentaria.ConfigBag.Application/Orcamentaria.ConfigBag.Application.csproj", "Orcamentaria.ConfigBag.Application/"]
COPY ["Orcamentaria.ConfigBag.Domain/Orcamentaria.ConfigBag.Domain.csproj", "Orcamentaria.ConfigBag.Domain/"]
COPY ["Orcamentaria.ConfigBag.Infrastructure/Orcamentaria.ConfigBag.Infrastructure.csproj", "Orcamentaria.ConfigBag.Infrastructure/"]

COPY nuget.config ./
COPY local-packages ./local-packages

RUN dotnet restore "Orcamentaria.ConfigBag.API/Orcamentaria.ConfigBag.API.csproj"

COPY . .

WORKDIR "/src/Orcamentaria.ConfigBag.API"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Orcamentaria.ConfigBag.API.dll"]
# ENV ASPNETCORE_URLS=http://+:5000
# EXPOSE 5000