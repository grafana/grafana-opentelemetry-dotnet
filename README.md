<!-- markdownlint-disable -->
<p>
  <img src="internal/img/Grafana_icon.png" alt="Grafana logo" height="70"/ >
  <img src="https://opentelemetry.io/img/logos/opentelemetry-logo-nav.png" alt="OpenTelemetry logo" width="70"/ >
</p>
<!-- markdownlint-enable -->

# Grafana OpenTelemetry distribution for .NET

<!-- markdown-link-check-disable -->
[![Build](https://github.com/grafana/grafana-opentelemetry-dotnet/actions/workflows/unit-tests.yml/badge.svg?branch=main)](https://github.com/grafana/grafana-opentelemetry-dotnet/actions/workflows/unit-tests.yml)
[![OATS](https://github.com/grafana/grafana-opentelemetry-dotnet/actions/workflows/oats.yml/badge.svg?branch=main)](https://github.com/grafana/grafana-opentelemetry-dotnet/actions/workflows/oats.yml)
[![Nuget](https://img.shields.io/nuget/v/Grafana.OpenTelemetry.svg)](https://www.nuget.org/profiles/Grafana)
[![SDK](https://img.shields.io/badge/otel--sdk-1.9.0-blue?style=flat&logo=opentelemetry)](https://github.com/open-telemetry/opentelemetry-dotnet)
[![Slack](https://img.shields.io/badge/join%20slack-%23app--o11y-brightgreen.svg?logo=slack)](https://grafana.slack.com/archives/C05E87XRK3J)
<!-- markdown-link-check-enable -->

## About

This is a pre-configured and pre-packaged bundle of [OpenTelemetry .NET components](http://github.com/open-telemetry/opentelemetry-dotnet-contrib),
optimized for [Grafana Cloud Application Observability](https://grafana.com/docs/grafana-cloud/monitor-applications/application-observability/).

It requires only minimal setup and configuration and makes it very easy to emit
OpenTelemetry traces, logs, and metrics from your .NET application.

## Getting Started

### Step 1: Install package

For installing the distribution with the full set of dependencies, add a
reference to the [`Grafana.OpenTelemetry`](https://www.nuget.org/packages/Grafana.OpenTelemetry)
package to your project.

```sh
dotnet add package --prerelease Grafana.OpenTelemetry
```

### Step 2: Enable the Grafana distribution at application startup

The `UseGrafana` extension method on the `TracerProviderBuilder` or the
`MetricProviderBuilder` can be used to set up the Grafana distribution. By
default, telemetry data will be sent to Grafana Alloy or an OTel collector
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

For details on how to obtain those values, refer to [Push directly from
applications using the OpenTelemetry
SDKs](https://grafana.com/docs/grafana-cloud/send-data/otlp/send-data-otlp/#push-directly-from-applications-using-the-opentelemetry-sdks).

```sh
export OTEL_EXPORTER_OTLP_PROTOCOL="http/protobuf"
export OTEL_EXPORTER_OTLP_ENDPOINT="https://otlp-gateway-prod-eu-west-0.grafana.net/otlp"
export OTEL_EXPORTER_OTLP_HEADERS="Authorization=Basic a-secret-token"
```

## Documentation

For detailed documentation and setup instructions, refer to the following
documents:

* [Installation](./docs/installation.md)
* [Configuration](./docs/configuration.md)
* [Supported instrumentations](./docs/supported-instrumentations.md)

## Troubleshooting

This project utilizes the [self-diagnostics feature of the .NET OpenTelemetry SDK](https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry/README.md#self-diagnostics).

To enable self-diagnostics, go to the
[current working directory](https://en.wikipedia.org/wiki/Working_directory) of
your process and create a configuration file named `OTEL_DIAGNOSTICS.json` with
the following content:

```json
{
    "LogDirectory": ".",
    "FileSize": 32768,
    "LogLevel": "Warning"
}
```

To disable self-diagnostics, delete the above file.

## Community

To engage with the Grafana Application Observability community:

* Chat with us on our community Slack channel. To invite yourself to the
  Grafana Slack, visit [https://grafana.slack.com/](https://grafana.slack.com)
  and join the [#application-observability](https://grafana.slack.com/archives/C05E87XRK3J)
  channel.
* Ask questions on the [Discussions page](https://github.com/grafana/grafana-opentelemetry-dotnet/discussions).
* [File an issue](https://github.com/grafana/grafana-opentelemetry-dotnet/issues/new)
  for bugs, issues, and feature suggestions.
