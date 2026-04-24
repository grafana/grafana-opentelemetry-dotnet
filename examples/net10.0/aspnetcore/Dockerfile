FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0.203@sha256:8a90a473da5205a16979de99d2fc20975e922c68304f5c79d564e666dc3982fc AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net10.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/aspnet:10.0.7-noble-chiseled-extra@sha256:b4855d6d1c557c19a7c7165b354a138b47c17b29669ad0b3b22fb046a1b84fd8 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
