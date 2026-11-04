FROM mcr.microsoft.com/dotnet/sdk:10.0 as build
WORKDIR /src

COPY ["Orcamentaria.ConfigBagService.API/Orcamentaria.ConfigBagService.API.csproj", "Orcamentaria.ConfigBagService.API/"]
COPY ["Orcamentaria.ConfigBagService.Application/Orcamentaria.ConfigBagService.Application.csproj", "Orcamentaria.ConfigBagService.Application/"]
COPY ["Orcamentaria.ConfigBagService.Domain/Orcamentaria.ConfigBagService.Domain.csproj", "Orcamentaria.ConfigBagService.Domain/"]
COPY ["Orcamentaria.ConfigBagService.Infrastructure/Orcamentaria.ConfigBagService.Infrastructure.csproj", "Orcamentaria.ConfigBagService.Infrastructure/"]

COPY nuget.config ./

RUN dotnet restore "Orcamentaria.ConfigBagService.API/Orcamentaria.ConfigBagService.API.csproj"

COPY . .

WORKDIR "/src/Orcamentaria.ConfigBagService.API"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Orcamentaria.ConfigBagService.API.dll"]
# ENV ASPNETCORE_URLS=http://+:5000
# EXPOSE 5000
