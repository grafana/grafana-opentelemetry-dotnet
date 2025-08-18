FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0.100-preview.7@sha256:3d69f7e851747ae8bd22509d7f54f036006edee67f73767213670fe570117d45 AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net10.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/aspnet:10.0.0-preview.7-noble-chiseled-extra@sha256:c3c8219bd68b8118c329ff2f4bb4c4c3f5481fa979ce1ebda4171c1196e040f7 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
