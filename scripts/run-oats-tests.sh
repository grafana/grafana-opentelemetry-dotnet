#!/usr/bin/env bash

set -euo pipefail

# TODO Pin to specific version and then update with renovate
export OATS_VERSION=latest

go install "github.com/grafana/oats@${OATS_VERSION}"
${GOPATH}/bin/oats --timeout=5m ./docker/docker-compose-aspnetcore
