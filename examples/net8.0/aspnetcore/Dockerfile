FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0.412@sha256:e6748a3b3d8cea753f643cacac0e577d8c409285b3924232db1354693bdbd3d7 AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net8.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/aspnet:8.0.18-noble-chiseled-extra@sha256:1f0890132c50c55879371a16261ef1a53d01c9bf14f9104f266eb91bb885e5d0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
