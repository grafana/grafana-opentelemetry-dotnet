# Migrating to OpenTelemetry .NET

All functionality provided by the distribution can also be utilized by manually
installing and configuring upstream OpenTelemetry .NET packages. Follow these
steps if you want to migrate from this distribution to the upstream
OpenTelemetry .NET project.

- Replace all environment variables or web.config/app.config values with the
"grafana" prefix as explained [here](https://grafana.com/docs/grafana-cloud/send-data/otlp/send-data-otlp/#push-directly-from-applications-using-the-opentelemetry-sdks)
- Install and activate the OpenTelemetry SDK,
as covered in the upstream [Getting Started](https://github.com/open-telemetry/opentelemetry-dotnet#getting-started)
guide
- Setup the OTLP exporter for metrics, logs, and traces - see the
[exporter README](https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry.Exporter.OpenTelemetryProtocol/README.md)
for details. The exporter will respect the previously set environment variables:
  - `OTEL_EXPORTER_OTLP_PROTOCOL`
  - `OTEL_EXPORTER_OTLP_ENDPOINT`
  - `OTEL_EXPORTER_OTLP_HEADERS`
- Install and configure any desired instrumentation packages
[listed here](./supported-instrumentations.md)
- Add `OTEL_SEMCONV_STABILITY_OPT_IN` environment variable to opt-in to the
latest HTTP semantic conventions
- [Add the recommended OpenTelemetry resource attributes](https://grafana.com/docs/opentelemetry/instrumentation/configuration/resource-attributes/) via the
`OTEL_RESOURCE_ATTRIBUTES` environment variable
  - `service.name`
  - `service.namespace`
  - `service.instance.id`
  - `service.version`
  - `deployment.environment`

```shell
export OTEL_SEMCONV_STABILITY_OPT_IN=http
export OTEL_RESOURCE_ATTRIBUTES=service.instance.id=<pod123>,deployment.environment=...
```
