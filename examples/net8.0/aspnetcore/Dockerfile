FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0.416@sha256:a87db299d259cec53210df406cd5e51f900a0c6d938fe56d5f392629c0505d75 AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net8.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/aspnet:8.0.22-noble-chiseled-extra@sha256:1ec4c9605a8fc97d3c2045d46557e51d9ca54ba4f91339205be2e44bfb184732 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
