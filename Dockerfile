FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Complevo/Complevo.csproj", "Complevo/"]
RUN dotnet restore "Complevo/Complevo.csproj"
COPY . .
WORKDIR "/src/Complevo"
RUN dotnet build "Complevo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Complevo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Complevo.dll"]
