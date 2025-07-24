# Grafana OpenTelemetry distribution for .NET

<!-- markdownlint-disable MD013 MD033 -->
<p>
  <img src="internal/img/Grafana_icon.png" alt="Grafana logo" height="70" />
  <img src="https://opentelemetry.io/img/logos/opentelemetry-logo-nav.png" alt="OpenTelemetry logo" width="70" />
</p>
<!-- markdownlint-enable MD013 MD033 -->

<!-- markdown-link-check-disable -->
[![Build][ci-badge]][ci-status]
[![OATS][oats-badge]][oats-status]
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

```sh
export OTEL_EXPORTER_OTLP_PROTOCOL="http/protobuf"
export OTEL_EXPORTER_OTLP_ENDPOINT="https://otlp-gateway-prod-eu-west-0.grafana.net/otlp"
export OTEL_EXPORTER_OTLP_HEADERS="Authorization=Basic base64-encoded-token"
```

> [!TIP]
> The token for `Basic` authentication is the base64-encoded value of a Grafana
> Cloud Instance ID and a service account token separated by a `:`.
>
> For example, if your Grafana Cloud Instance ID is `12345` and your token is
> `glc_secret`, the value for `OTEL_EXPORTER_OTLP_HEADERS` would be
> `Authorization=Basic MTIzNDU6Z2xjX3NlY3JldA==`

## Documentation

For detailed documentation and setup instructions, refer to the following
documents:

* [Installation](./docs/installation.md)
* [Configuration](./docs/configuration.md)
* [Supported instrumentations](./docs/supported-instrumentations.md)
* [Supported resource detectors](./docs/supported-resource-detectors.md)
* [Migrating to upstream](./docs/migration.md)

## Troubleshooting

This project utilizes the [self-diagnostics feature of the .NET OpenTelemetry SDK][self-diagnostics].

To enable self-diagnostics, go to the [current working directory][working-dir] of
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
  Grafana Slack, visit <https://grafana.slack.com>
  and join the [#application-observability][slack-channel] channel.
* Ask questions on the [Discussions page][discussions].
* [File an issue][issues] for bugs, issues, and feature suggestions.

[app-o11y]: https://grafana.com/docs/grafana-cloud/monitor-applications/application-observability/
[ci-badge]: https://github.com/grafana/grafana-opentelemetry-dotnet/actions/workflows/ci.yml/badge.svg?branch=main
[ci-status]: https://github.com/grafana/grafana-opentelemetry-dotnet/actions/workflows/ci.yml
[discussions]: https://github.com/grafana/grafana-opentelemetry-dotnet/discussions
[issues]: https://github.com/grafana/grafana-opentelemetry-dotnet/issues/new
[oats-badge]: https://github.com/grafana/grafana-opentelemetry-dotnet/actions/workflows/oats.yml/badge.svg?branch=main
[oats-status]: https://github.com/grafana/grafana-opentelemetry-dotnet/actions/workflows/oats.yml
[otel]: https://github.com/open-telemetry/opentelemetry-dotnet
[otel-badge]: https://img.shields.io/badge/OTel--SDK-1.9.0-blue?style=flat&logo=opentelemetry
[otel-contrib]: http://github.com/open-telemetry/opentelemetry-dotnet-contrib
[package]: https://www.nuget.org/packages/Grafana.OpenTelemetry
[package-badge-version]: https://img.shields.io/nuget/v/Grafana.OpenTelemetry?logo=nuget&label=NuGet&color=blue
[package-download]: https://www.nuget.org/profiles/Grafana
[push-oltp]: https://grafana.com/docs/grafana-cloud/send-data/otlp/send-data-otlp/#quickstart-architecture
[self-diagnostics]: https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry/README.md#self-diagnostics
[slack-badge]: https://img.shields.io/badge/%20Slack-%23app--o11y-brightgreen.svg?logo=slack
[slack-channel]: https://grafana.slack.com/archives/C05E87XRK3J
[working-dir]: https://en.wikipedia.org/wiki/Working_directory
