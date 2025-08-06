FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0.100-preview.6@sha256:3deb1cdd2ca8a1d63ee473d868c626e37f3f6629fef65263a60a714916d3da2d AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net10.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/aspnet:10.0.0-preview.6-noble-chiseled-extra@sha256:c0b77d8cb86373d0ef03801085b359f63fa7ae69ecefae08c5df25b832c1b937 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
