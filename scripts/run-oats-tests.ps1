#! /usr/bin/env pwsh

$ErrorActionPreference = "Stop"
$InformationPreference = "Continue"
$ProgressPreference = "SilentlyContinue"

# TODO Pin to specific version and then update with renovate
${env:OATS_VERSION}="latest"

go install "github.com/grafana/oats@${env:OATS_VERSION}"
& "${env:GOPATH}/bin/oats" --timeout=5m ./docker/docker-compose-aspnetcore
