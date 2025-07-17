# Migrating to OpenTelemetry .NET

All functionality provided by the distribution can also be utilized by manually
installing and configuring upstream OpenTelemetry .NET packages. Follow these
steps if you want to migrate from this distribution to the upstream
OpenTelemetry .NET project.

- Set appropriate environment variables or `web.config`/`app.config` values as
explained here: [Send data to the Grafana Cloud OTLP endpoint][publish-otlp]
- Install and activate the OpenTelemetry SDK,
as covered in the upstream [Getting Started][getting-started] guide
- Setup the OTLP exporter for metrics, logs, and traces - see the
[exporter README][exporter-readme] for details. The exporter will respect the
previously set environment variables:
  - `OTEL_EXPORTER_OTLP_PROTOCOL`
  - `OTEL_EXPORTER_OTLP_ENDPOINT`
  - `OTEL_EXPORTER_OTLP_HEADERS`
- Install and configure any desired instrumentation packages
  [listed here](./supported-instrumentations.md)
- [Add the OpenTelemetry resource attributes][resource-attributes]
via the `OTEL_RESOURCE_ATTRIBUTES` environment variable
  - `service.name`
  - `service.namespace`
  - `service.instance.id`
  - `service.version`
  - `deployment.environment`

```shell
export OTEL_RESOURCE_ATTRIBUTES=service.instance.id=<pod123>,deployment.environment=...
```

[exporter-readme]: https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry.Exporter.OpenTelemetryProtocol/README.md
[getting-started]: https://github.com/open-telemetry/opentelemetry-dotnet#getting-started
[publish-otlp]: https://grafana.com/docs/grafana-cloud/send-data/otlp/send-data-otlp/#quickstart-architecture
[resource-attributes]: https://grafana.com/docs/grafana-cloud/monitor-applications/application-observability/setup/resource-attributes/
