#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Zelda/src/Zelda.csproj", "Zelda/"]
RUN dotnet restore "Zelda/src/Zelda.csproj"
COPY . .
WORKDIR "/src/Zelda"
RUN dotnet build "Zelda.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zelda.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zelda.dll"]