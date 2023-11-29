# Changelog

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
