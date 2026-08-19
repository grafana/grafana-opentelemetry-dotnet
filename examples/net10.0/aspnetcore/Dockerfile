FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:11.0.100-preview.7@sha256:fd96d62893be03527912ce16b32e4331a64e6cf93393129f783a59b11e651790 AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net10.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/aspnet:11.0.0-preview.7-resolute-chiseled-extra@sha256:9d9fb8ebca958c66623f9c841818bd32aac717fdbb9a0d119c80cec965ac01af AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
