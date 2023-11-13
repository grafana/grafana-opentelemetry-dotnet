# Migrating to OpenTelemetry .NET

Follow these steps if you want to migrate from this distribution to the
upstream OpenTelemetry .NET project:

- Replace all environment variables or system properties with the "grafana"
prefix as explained [here](https://grafana.com/docs/grafana-cloud/send-data/otlp/send-data-otlp/#push-directly-from-applications-using-the-opentelemetry-sdks).
- Install and activate the OpenTelemetry SDK,
[as covered in the upstream Getting Started guide](https://github.com/open-telemetry/opentelemetry-dotnet#getting-started)
- Install any desired instrumentation packages
[listed here](./supported-instrumentations.md)
- Add `OTEL_SEMCONV_STABILITY_OPT_IN` environment variable to opt-in to the
latest HTTP semantic conventions
- [Add the recommended OpenTelemetry resource attributes](https://grafana.com/docs/opentelemetry/instrumentation/configuration/resource-attributes/)
  - `service.namespace`
  - `service.name`
  - `deployment.environment`
  - `service.instance.id`
  - `service.service`

```shell
export OTEL_SEMCONV_STABILITY_OPT_IN=http
export OTEL_RESOURCE_ATTRIBUTES=service.instance.id=<pod123>,deployment.environment=...
```
