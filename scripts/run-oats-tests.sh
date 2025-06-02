#!/usr/bin/env bash

set -euo pipefail

# TODO Pin to specific version and then update with renovate
export OATS_VERSION=latest

export TESTCASE_SKIP_BUILD=true
export TESTCASE_TIMEOUT=5m
export TESTCASE_BASE_PATH=./docker

go install "github.com/grafana/oats@${OATS_VERSION}"
${GOPATH}/bin/oats ./docker/docker-compose-aspnetcore
