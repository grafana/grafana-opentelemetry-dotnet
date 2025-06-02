#! /usr/bin/env pwsh

$ErrorActionPreference = "Stop"
$InformationPreference = "Continue"
$ProgressPreference = "SilentlyContinue"

# TODO Pin to specific version and then update with renovate
${env:OATS_VERSION}="173ac1f6a190c2ffd156a98dfe923c70a2a0c3ca"

${env:TESTCASE_SKIP_BUILD}="true"
${env:TESTCASE_TIMEOUT}="5m"
${env:TESTCASE_BASE_PATH}="./docker"

go install "github.com/grafana/oats@${env:OATS_VERSION}"
& "${env:GOPATH}/bin/oats" ./docker/docker-compose-aspnetcore
