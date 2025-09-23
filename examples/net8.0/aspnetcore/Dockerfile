FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0.414@sha256:3cef19377b2ef2a0171e930a24627677447c3e41b5f2eab84ff4895f1b15d254 AS build
ARG TARGETARCH
ARG CONFIGURATION="Release"
ARG DOTNET_PUBLISH_ARGS=""

COPY . /source
WORKDIR /source

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "examples/net8.0/aspnetcore/aspnetcore.csproj" --arch "${TARGETARCH}" --configuration "${CONFIGURATION}" --output /app --self-contained true ${DOTNET_PUBLISH_ARGS}

FROM mcr.microsoft.com/dotnet/runtime:8.0.20-azurelinux3.0-distroless-extra@sha256:46fedd5b7a7713ba96a27b1744317b34ea89f93d408becef4fc6c129283f6e36 AS final
WORKDIR /app
EXPOSE 8080
USER $APP_UID

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

COPY --from=build /app .
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
