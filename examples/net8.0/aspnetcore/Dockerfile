FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0.413@sha256:c0e30f202cfe2461b2755235f15fbe782e82e56da9f43fc5e2c5eed9e1c6297d AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net8.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/aspnet:8.0.19-noble-chiseled-extra@sha256:f27f42210a09b63d8e16e3f2753a7f05983f58bfd2eaa233c7c6a42c5b8cc5b4 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
