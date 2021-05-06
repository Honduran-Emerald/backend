FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Emerald.Application/Emerald.Application.csproj", "Emerald.Application/"]
COPY ["Emerald.Domain/Emerald.Domain.csproj", "Emerald.Domain/"]
COPY ["Emerald.Infrastructure/Emerald.Infrastructure.csproj", "Emerald.Infrastructure/"]
RUN dotnet restore "Emerald.Application/Emerald.Application.csproj"
COPY . .
WORKDIR "/src/Emerald.Application"
RUN dotnet build "Emerald.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Emerald.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Emerald.Application.dll"]