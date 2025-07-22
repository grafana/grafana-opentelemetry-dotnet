#!/usr/bin/env bash

set -euo pipefail

# renovate: datasource=github-releases depName=oats packageName=grafana/oats
export OATS_VERSION=v0.4.0

go install "github.com/grafana/oats@${OATS_VERSION}"
${GOPATH}/bin/oats --timeout=5m ./docker/docker-compose-aspnetcore
