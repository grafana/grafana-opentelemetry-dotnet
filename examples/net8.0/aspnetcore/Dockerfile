FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0.413@sha256:ff90bfa1e984430ae1109003dfa2df81b5e715c4d790bbf806dc3d927d71626f AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net8.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/aspnet:8.0.19-noble-chiseled-extra@sha256:a8be11432b74c6ec08ffd8da94d2bc9dd4ead265f59f83cb437c33c91d6a2c30 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
