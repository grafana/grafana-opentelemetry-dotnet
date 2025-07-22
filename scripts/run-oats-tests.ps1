#! /usr/bin/env pwsh

$ErrorActionPreference = "Stop"
$InformationPreference = "Continue"
$ProgressPreference = "SilentlyContinue"

# renovate: datasource=github-releases depName=oats packageName=grafana/oats
${env:OATS_VERSION}="v0.4.0"

go install "github.com/grafana/oats@${env:OATS_VERSION}"
& "${env:GOPATH}/bin/oats" --timeout=5m ./docker/docker-compose-aspnetcore
