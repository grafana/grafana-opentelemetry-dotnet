FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0.101@sha256:5504edd1267dd4deab3443f960cfab219249c8bd935fbcc358f1c24aeae23fe0 AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net10.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/aspnet:10.0.1-noble-chiseled-extra@sha256:9f1788f0dc1f3db11143a80b2b68b7612bf64bd23f367801bcbbf1861cce06c0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
