#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["1 - Application/IHunger.WebAPI/IHunger.WebAPI.csproj", "1 - Application/IHunger.WebAPI/"]
RUN dotnet restore "1 - Application/IHunger.WebAPI/IHunger.WebAPI.csproj"
COPY . .
WORKDIR "/src/1 - Application/IHunger.WebAPI"
RUN dotnet build "IHunger.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IHunger.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IHunger.WebAPI.dll"]