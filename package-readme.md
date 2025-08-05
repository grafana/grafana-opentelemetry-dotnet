# Grafana OpenTelemetry distribution for .NET

<!-- markdown-link-check-disable -->
[![NuGet][package-badge-version]][package-download]
[![SDK][otel-badge]][otel]
[![Slack][slack-badge]][slack-channel]
<!-- markdown-link-check-enable -->

## About

This is a pre-configured and pre-packaged bundle of [OpenTelemetry .NET components][otel-contrib],
optimized for [Grafana Cloud Application Observability][app-o11y].

It requires only minimal setup and configuration and makes it very easy to emit
OpenTelemetry metrics, logs, and traces from your .NET application.

## Getting Started

### Step 1: Install package

For installing the distribution with the full set of dependencies, add a
reference to the [`Grafana.OpenTelemetry`][package] package to your project.

```sh
dotnet add package Grafana.OpenTelemetry
```

### Step 2: Enable the Grafana distribution at application startup

The `UseGrafana` extension method on the `TracerProviderBuilder` or the
`MetricProviderBuilder` can be used to set up the Grafana distribution. By
default, telemetry data will be sent to Grafana Alloy or an OpenTelemetry collector
that runs locally and listens to default OTLP ports.

```csharp
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .UseGrafana()
    .Build();
```

Alternatively, you can send telemetry data directly to Grafana Cloud without
involving an agent or collector. This can be configured via the environment
variables `OTEL_EXPORTER_OTLP_PROTOCOL`, `OTEL_EXPORTER_OTLP_ENDPOINT`, and
`OTEL_EXPORTER_OTLP_HEADERS`.

For details on how to obtain those values, refer to
[Send data to the Grafana Cloud OTLP endpoint: Quickstart architecture][push-oltp].

## Documentation

For detailed documentation and setup instructions, refer to [our documentation][docs].

[app-o11y]: https://grafana.com/docs/grafana-cloud/monitor-applications/application-observability/
[docs]: https://github.com/grafana/grafana-opentelemetry-dotnet/tree/main/docs
[otel]: https://github.com/open-telemetry/opentelemetry-dotnet
[otel-badge]: https://img.shields.io/badge/OTel--SDK-1.12.0-blue?style=flat&logo=opentelemetry
[otel-contrib]: http://github.com/open-telemetry/opentelemetry-dotnet-contrib
[package]: https://www.nuget.org/packages/Grafana.OpenTelemetry
[package-badge-version]: https://img.shields.io/nuget/v/Grafana.OpenTelemetry?logo=nuget&label=NuGet&color=blue
[package-download]: https://www.nuget.org/profiles/Grafana
[push-oltp]: https://grafana.com/docs/grafana-cloud/send-data/otlp/send-data-otlp/#quickstart-architecture
[slack-badge]: https://img.shields.io/badge/%20Slack-%23app--o11y-brightgreen.svg?logo=slack
[slack-channel]: https://grafana.slack.com/archives/C05E87XRK3J
