# Changelog

## Unreleased

### Bug fixes

* Use 1.7.1 of ASP.NET Core instrumentation.
  * Fixed issue
  [#4466](https://github.com/open-telemetry/opentelemetry-dotnet/issues/4466)
  where the activity instance returned by `Activity.Current` was different than
  instance obtained from `IHttpActivityFeature.Activity`.
  ([#5136](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5136))

  * Fixed an issue where the `http.route` attribute was not set on either the
  `Activity` or `http.server.request.duration` metric generated from a
  request when an exception handling middleware is invoked. One caveat is that
  this fix does not address the problem for the `http.server.request.duration`
  metric when running ASP.NET Core 8. ASP.NET Core 8 contains an equivalent fix
  which should ship in version 8.0.2
  (see: [dotnet/aspnetcore#52652](https://github.com/dotnet/aspnetcore/pull/52652)).
  ([#5135](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5135))

  * Fixes scenario when the `net6.0` target of this library is loaded into a
  .NET 7+ process and the instrumentation does not behave as expected. This
  is an unusual scenario that does not affect users consuming this package
  normally. This fix is primarily to support the
  [opentelemetry-dotnet-instrumentation](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5252)
  project.
  ([#5252](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5252))

* Use 1.7.1 of HTTP instrumentation.
  * .NET Framework - fix description for `http.client.request.duration` metric.
  ([#5234](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5234))

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
