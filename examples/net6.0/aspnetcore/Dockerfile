#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG DOTNET_PUBLISH_ARGS=""

WORKDIR /src
COPY ["examples/net6.0/aspnetcore/aspnetcore.csproj", "examples/net6.0/aspnetcore/"]
RUN dotnet restore "examples/net6.0/aspnetcore/aspnetcore.csproj"
COPY . .
WORKDIR "/src/examples/net6.0/aspnetcore"
RUN dotnet build "aspnetcore.csproj" -c Release -o /app/build

FROM build AS publish
RUN echo "/app/publish ${DOTNET_PUBLISH_ARGS}"
RUN dotnet publish "aspnetcore.csproj" -c Release -o /app/publish ${DOTNET_PUBLISH_ARGS}

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
