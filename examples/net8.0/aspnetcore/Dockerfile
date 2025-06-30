FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0.411@sha256:e6a5a8d884609907fa8d468b927df765967f6b22f890ce92bd3ae614ca4ae87e AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net8.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/aspnet:8.0.17-noble-chiseled-extra@sha256:db790153de8350a1809b62aa495364372df4c9b551e9606a1bd857908299e2b9 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
