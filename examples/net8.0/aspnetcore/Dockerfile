FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0.415@sha256:4632e98dc703311ca09d0c37170fe70ad4ba334bd88a841c78cbcc9d29994b2c AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net8.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/aspnet:8.0.21-noble-chiseled-extra@sha256:0dd9895157db48565f461e7a247bb5685f00c89d8d1f3113b6dd9a91acb1ffa3 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
