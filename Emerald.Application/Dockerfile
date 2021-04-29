#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
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