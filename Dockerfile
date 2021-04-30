FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /work

#COPY ["Emerald.Application/Emerald.Application.csproj", "Emerald.Application/"]
#COPY ["Emerald.Domain/Emerald.Domain.csproj", "Emerald.Domain/"]
#COPY ["Emerald.Infrastructure/Emerald.Infrastructure.csproj", "Emerald.Infrastructure/"]

COPY */*.csproj ./
RUN for projectFile in $(ls *.csproj); \
do \
  mkdir -p ${projectFile%.*}/ && mv $projectFile ${projectFile%.*}/; \
done
RUN dotnet restore "Emerald.Application/Emerald.Application.csproj" --disable-parallel

COPY . .
#WORKDIR "/src/Emerald.Application"
#RUN dotnet build "Emerald.Application.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR /work/Emerald.Application
RUN dotnet publish -c Development -o /app --no-restore
#RUN dotnet publish "Emerald.Application.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "Emerald.Application.dll"]
