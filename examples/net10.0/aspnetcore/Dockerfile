FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:11.0-preview@sha256:c5aa0a301759a1b07654450e10e3ec78c3d476d0537263ca1ab5850d9f302fca AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net10.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/aspnet:11.0-preview-resolute-chiseled-extra@sha256:f6734c0ed950db463ba1ad5a2be281347df01f46ff3969399222e13093399dec AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
