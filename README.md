<!-- markdownlint-disable -->
<p>
  <img src="internal/img/Grafana_icon.png" alt="Grafana logo" height="70"/ >
  <img src="https://opentelemetry.io/img/logos/opentelemetry-logo-nav.png" alt="OpenTelemetry logo" width="70"/ >
</p>
<!-- markdownlint-enable -->

# Grafana OpenTelemetry distribution for .NET

<!-- markdown-link-check-disable -->
[![Build](https://github.com/grafana/grafana-opentelemetry-dotnet/actions/workflows/unit-tests.yml/badge.svg?branch=main)](https://github.com/grafana/grafana-opentelemetry-dotnet/actions/workflows/unit-tests.yml)
[![Nuget](https://img.shields.io/nuget/v/Grafana.OpenTelemetry.svg)](https://www.nuget.org/profiles/Grafana)
[![SDK](https://img.shields.io/badge/otel--sdk-1.6.0-blue?style=flat&logo=opentelemetry)](https://github.com/open-telemetry/opentelemetry-dotnet)
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
default, telemetry data will be sent to a Grafana agent or an OTel collector
that runs locally and listens to default OTLP ports.

Given the zone, instance id, and API token, telemetry data can be sent directly
to the Grafana Cloud without involving an agent or collector:

```csharp
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .UseGrafana(config =>
    {
        config.ExporterSettings = new CloudOtlpExporter
        {
            Zone = "prod-us-east-0",
            InstanceId = "123456",
            ApiKey = "a-secret-token"
        };
    })
    .Build();
```

For details on how to obtain those values, refer to [Sending data directly to
Grafana Cloud via
OTLP](./docs/configuration.md#sending-data-directly-to-grafana-cloud-via-otlp).

Alternatively, these values can be set via the environment variables
`GRAFANA_CLOUD_ZONE`, `GRAFANA_CLOUD_INSTANCE_ID`, and
`GRAFANA_CLOUD_API_KEY`.

```sh
export GRAFANA_CLOUD_ZONE=prod-us-east-0
export GRAFANA_CLOUD_INSTANCE_ID=123456
export GRAFANA_CLOUD_API_KEY=a-secret-token
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
  Grafana Slack, visit [https://slack.grafana.com/](https://slack.grafana.com)
  and join the [#application-observability](https://grafana.slack.com/archives/C05E87XRK3J)
  channel.
* Ask questions on the [Discussions page](https://github.com/grafana/grafana-opentelemetry-dotnet/discussions).
* [File an issue](https://github.com/grafana/grafana-opentelemetry-dotnet/issues/new)
  for bugs, issues, and feature suggestions.
