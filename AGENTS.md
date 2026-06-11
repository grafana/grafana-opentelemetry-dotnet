# Coding Agent Instructions

<!-- markdownlint-disable MD013 MD033 MD041 MD051 MD059 -->

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

This repository builds the **Grafana OpenTelemetry distribution for .NET**: a pre-configured,
pre-packaged bundle of upstream OpenTelemetry .NET components optimized for Grafana Cloud
Application Observability. It ships two NuGet packages and exposes a `UseGrafana` extension that
wires up exporters, instrumentations, and resource detectors with minimal user configuration.

## Common commands

The solution file is `GrafanaOpenTelemetry.slnx`. Build and test from the repository root.

```sh
dotnet build --configuration Release          # builds and (via GeneratePackageOnBuild) packs to ./artifacts
dotnet test --configuration Release           # runs the xUnit test suite across all target frameworks
dotnet format --verify-no-changes             # the same format check enforced in CI
```

Run a single test or a subset with a filter:

```sh
dotnet test --filter "FullyQualifiedName~InstrumentationTest"
dotnet test --framework net10.0 --filter "DisplayName~MyTestName"
```

Build output goes to `./artifacts` (`UseArtifactsOutput` is enabled), not per-project `bin/obj`
package output — built `.nupkg`/`.snupkg` files land in `./artifacts/package/release`.

OATS acceptance tests (end-to-end, require Go and Docker) live under `docker/` and are launched by
`scripts/run-oats-tests.ps1` / `scripts/run-oats-tests.sh`, which install the `grafana/oats` tool
and run the compose scenarios.

## Target frameworks and SDK

- The libraries multi-target `net10.0;net8.0;netstandard2.0;net462`. The test project targets
  `net10.0;net8.0` plus `net481` on Windows only.
- `global.json` pins the SDK to `10.0.300` (roll-forward `latestFeature`). CI also installs the
  .NET 8 SDK because builds touch `net8.0`.
- `net8.0`+ builds set `IsAotCompatible`, but the public `UseGrafana` APIs are annotated
  `RequiresUnreferencedCode` — the distribution does **not** support native AOT, because
  instrumentation is wired up via reflection (see below).
- `TreatWarningsAsErrors` is on and `Nullable` is **disabled** repo-wide (`Directory.Build.props`).

## Architecture

### Two packages: `Base` (minimal) and `Grafana.OpenTelemetry` (full)

- `src/Grafana.OpenTelemetry.Base` contains **all** the distribution code: the `UseGrafana`
  extensions, settings, exporters, and every instrumentation/resource-detector initializer. It only
  references OpenTelemetry instrumentation packages that bring **no extra transitive dependencies**
  (HTTP, Runtime, SqlClient, gRPC, Process, and the resource detectors).
- `src/Grafana.OpenTelemetry` is the **full meta-package**. It references `Base` and adds the
  heavyweight instrumentation packages that pull in third-party dependencies (AWS, ASP.NET / ASP.NET
  Core, Cassandra, Elasticsearch, EF Core, Hangfire, Quartz, Redis, WCF, Owin). It contains almost
  no code beyond `OpenTelemetryBuilderExtension`.

This split lets consumers who want a small dependency surface depend on `Base`, while most consumers
take the full package.

### Reflection-based instrumentation activation

Because `Base` holds the code but not all instrumentation packages, instrumentation is enabled at
runtime through reflection rather than direct method calls:

- Each instrumentation is an `InstrumentationInitializer` subclass (e.g. `StackExchangeRedisInitializer`)
  in `Base/Instrumentations/`. Its `InitializeTracing`/`InitializeMetrics` override calls
  `ReflectionHelper.CallStaticMethod(...)` to invoke the upstream `Add*Instrumentation` extension —
  so it no-ops gracefully (logged via `GrafanaOpenTelemetryEventSource`) when the package is absent.
- `InstrumentationInitializer.Initializers` is the static registry of all initializers; framework
  conditionals (`#if NETFRAMEWORK`, AOT/`RuntimeFeature.IsDynamicCodeSupported` for SqlClient)
  decide which are registered. Resource detectors follow the same pattern in
  `Base/ResourceDetectors/` via `ResourceDetectorInitializer`.
- When adding a new instrumentation or resource detector: add the `Instrumentation`/`ResourceDetector`
  enum value, create an initializer, register it in the `Initializers` array, add the package
  reference to the correct project (`Base` only if it has no extra dependencies, otherwise the full
  package), and update `docs/supported-instrumentations.md` / `docs/supported-resource-detectors.md`.

### Entry points and configuration

- `UseGrafana` extends `TracerProviderBuilder` and `MeterProviderBuilder`
  (`Base/TracerProviderBuilderExtensions.cs`, `MeterProviderBuilderExtensions.cs`), plus
  `OpenTelemetryLoggerOptions`. The full package adds an `IOpenTelemetryBuilder.UseGrafana` overload
  (`OpenTelemetryBuilderExtension.cs`) for ASP.NET Core hosting.
- `GrafanaOpenTelemetrySettings` is the configuration model. Defaults are read from environment
  variables (e.g. `OTEL_SERVICE_NAME`, `GRAFANA_DOTNET_DISABLE_INSTRUMENTATIONS`,
  `GRAFANA_DOTNET_RESOURCE_DETECTORS`, `DOTNET_ENVIRONMENT`/`ASPNETCORE_ENVIRONMENT`). By default all
  instrumentations are enabled (except `AWSLambda`, which is removed by default) and only
  low-startup-cost resource detectors run (Container is intentionally disabled by default).
- Exporters live in `Base/ExporterSettings/`. `UseGrafana` defaults to a `CloudOtlpExporter`
  (configured from `OTEL_EXPORTER_OTLP_*` env vars) and falls back to an `AgentOtlpExporter`
  (local Alloy/Collector) when those aren't present.

### AOT note

`src/Grafana.OpenTelemetry/rd.xml` is a runtime-directives file kept to avoid reflection problems
under AOT/trimming for the full package.

## Versioning and releases

Package versions come from **MinVer** (driven by git tags); don't hand-edit version numbers in
`.csproj` files. Releasing is documented in `RELEASING.md`, and user-facing changes must be recorded
in `CHANGELOG.md`.

## CI and linting

- `ci.yml` builds and tests on macOS, Linux, and Windows, then validates and publishes the NuGet
  packages. `dotnet-format.yml` enforces `dotnet format --verify-no-changes`. `oats.yml` runs the
  acceptance tests.
- `lint.yml` runs `actionlint` on workflows, `markdownlint` (`.markdownlint.yaml`), and
  `markdown-link-check` (`.markdown_link_check_config.json`) on Markdown.

## Documentation style

`.github/copilot-instructions.md` is a detailed **Grafana Labs documentation style guide** that
applies to all `**/*.md` files. When writing or editing docs (under `docs/`, `README.md`, etc.),
follow it: sentence-case headings, present-tense active voice, address the reader as "you", dashes
(never asterisks) for unordered lists, ordered lists that always start each item with `1.`, use full
product names ("OpenTelemetry", not "OTel"), and never strip YAML front matter. Refer to that file
before substantial documentation work.
