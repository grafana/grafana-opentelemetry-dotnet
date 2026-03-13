FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0.201@sha256:478b9038d187e5b5c29bfa8173ded5d29e864b5ad06102a12106380ee01e2e49 AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net10.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/aspnet:10.0.5-noble-chiseled-extra@sha256:a04d1c1d2d26119049494057d80ea6cda25bbd8aef7c444a1fc1ef874fd3955b AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
