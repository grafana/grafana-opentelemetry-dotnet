FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0.414@sha256:ff8311847c54c04d1a14c488362807997d59b61372da5095a95f89cbcda7f9b7 AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net8.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/aspnet:8.0.20-noble-chiseled-extra@sha256:e396ddb631cb148b3db17b8a464e2a306f4e802381c14e779362fb44aeb3cf5c AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
