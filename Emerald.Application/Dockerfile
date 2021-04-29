FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Emerald.Application/Emerald.Application.csproj", "Emerald.Application/"]
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
