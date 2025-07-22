FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0.412@sha256:4763fe4aeb0c5f0dec72fe29678329113f9ee5d4f092a86a5a5044c0bb18e974 AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net8.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/aspnet:8.0.18-noble-chiseled-extra@sha256:479b9a3f71f169960d909f599f1b0da82af99bbd6b1179216923eb730217db0d AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
