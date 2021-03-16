FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY "ProductAvailabilityService.csproj" .
RUN dotnet restore "ProductAvailabilityService.csproj"
COPY . .
RUN dotnet build "ProductAvailabilityService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductAvailabilityService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductAvailabilityService.dll"]