FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0.202@sha256:adc02be8b87957d07208a4a3e51775935b33bad3317de8c45b1e67357b4c073b AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net10.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/aspnet:10.0.6-noble-chiseled-extra@sha256:d99c7381d67e24e249a8dbb9bf720deeedca7655afc5bbc683c2af0ea083eb61 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
