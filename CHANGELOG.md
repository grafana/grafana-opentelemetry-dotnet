# Changelog

## Unreleased

### New features

* [#81](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/81)
  Adds a .NET 8 test project and integrates it into the OATS test matrix.
* [#85](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/85)
  Adds resource detectors for Azure, host, process, process runtime, and
  container resource attributes.
* [#87](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/87)
  Add a new `OtlpExporter` class supporting specifying OTLP protocol, endpoint,
  and header

### Deprecation

* [#87](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/87)
  Deprecate use of `GRAFANA_CLOUD_*` environment variables in code and documentation

  The use of `CloudOtlpExporter` and `GRAFANA_CLOUD_*` environment variables is
  deprecated. Instead use `OtlpExporter` for code-level configuration, and
  `OTEL_EXPORTER_OTLP_PROTOCOL`, `OTEL_EXPORTER_OTLP_ENDPOINT`, and
  `OTEL_EXPORTER_OTLP_HEADERS` for configuration via environment variables.

## 0.7.0-beta.3

### Bug fixes

* Sets `OTEL_DOTNET_EXPERIMENTAL_ASPNETCORE_ENABLE_GRPC_INSTRUMENTATION`
  to `true` when enabling tracing. [gRPC instrumentation is experimental](https://github.com/open-telemetry/opentelemetry-dotnet/releases/tag/Instrumentation.AspNetCore-1.6.0),
  while HTTP is stable.

## 0.7.0-beta.2

### Bug fixes

* [#71](https://github.com/grafana/grafana-opentelemetry-dotnet/issues/71):
  Lazy-loading of ASP.NET Core instrumentation was broken. This was fixed by
  updateing changed class names of ASP.NET Core instrumentation library
  extension classes.

## 0.7.0-beta.1

### BREAKING CHANGES

* Use 1.7.0 of ASP.NET Core instrumentation.
  * Removes support for `OTEL_SEMCONV_STABILITY_OPT_IN`.
    [Instrumentation will only emit stable conventions](https://github.com/open-telemetry/semantic-conventions/tree/v1.23.0/docs/http).
  * Defaults `OTEL_DOTNET_EXPERIMENTAL_ASPNETCORE_ENABLE_GRPC_INSTRUMENTATION`
    to `true`. [gRPC instrumentation is experimental](https://github.com/open-telemetry/opentelemetry-dotnet/releases/tag/Instrumentation.AspNetCore-1.6.0),
    while HTTP is stable.
* Use 1.7.0 of HTTP instrumentation.
  * [`http.user_agent` Activity tag removed from HTTP instrumentation](https://github.com/open-telemetry/opentelemetry-dotnet/releases/tag/1.6.0-rc.1).
  * Removes support for `OTEL_SEMCONV_STABILITY_OPT_IN`.
    [Instrumentation will only emit stable conventions](https://github.com/open-telemetry/semantic-conventions/tree/v1.23.0/docs/http).

### New features

* [Use 1.7.0 of upstream SDK](https://github.com/open-telemetry/opentelemetry-dotnet/releases/tag/Instrumentation.AspNetCore-1.7.0).

## 0.6.0-beta.3

### Bug fixes

* [#65](https://github.com/grafana/grafana-opentelemetry-dotnet/issues/65):
  Using the distribution broke self-contained applications. This has been fixed
  by using a different way to determine the distribution version.
* [#64](https://github.com/grafana/grafana-opentelemetry-dotnet/issues/64): The
  version in `telemetry.distro.version` contained a trailing commit hash. This
  commit hash is removed now before populating the attribute.

## 0.6.0-beta.2

### New features

* Use 1.6.0-beta.3 of upstream instrumentation libraries.
* Allow specifying custom resource attributes via `GrafanaOpenTelemetrySettings`.
* Run unit tests on .NET 8.
* Use libraries released with .NET 8.
* Improve accuracy of resource attributes `telemetry.distro.name` and `telemetry.distro.version`.

### Bug fixes

* Make unit tests runnable for non-net462 targets.

## 0.6.0-beta.1

Released 2023-11-09

* Initial public preview release
