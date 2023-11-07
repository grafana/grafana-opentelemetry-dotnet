# Grafana distribution of OpenTelemetry .NET instrumentation

[![Slack](https://img.shields.io/badge/join%20slack-%23application-observability-brightgreen.svg?logo=slack)](https://grafana.slack.com/archives/C05E87XRK3J)
[![Nuget](https://img.shields.io/nuget/v/Grafana.OpenTelemetry.svg)](https://www.nuget.org/profiles/Grafana)
[![Build](https://github.com/grafana/grafana-opentelemetry-dotnet/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/grafana/grafana-opentelemetry-dotnet/actions/workflows/ci.yml)

* [About](#about)
* [Getting Started](#getting-started)
* [Installation](#getting-started)
* [Configuration](#configuration)
* [Supported instrumentations](#supported-instrumentations)
* [Troubleshooting](#troubleshooting)
# [Community](#community)

## About

This is a pre-configured and pre-packaged bundle of [OpenTelemetry .NET components](http://github.com/open-telemetry/opentelemetry-dotnet),
optimized for [Grafana Cloud Application Observability](https://grafana.com/docs/grafana-cloud/monitor-applications/application-observability/).

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
that runs locally and listens to default OTLP ports:

```csharp
using OpenTelemetry;
using OpenTelemetry.Trace;
using Grafana.OpenTelemetry;

public class Program
{
    public static void Main(string[] args)
    {
        using var tracerProvider = Sdk.CreateTracerProviderBuilder()
            .UseGrafana()
            .Build();
    }
}
```

## Installation

### Supported .NET Versions

The packages shipped from this repository generally support all the officially
supported versions of [.NET](https://dotnet.microsoft.com/download/dotnet) and
[.NET Framework](https://dotnet.microsoft.com/download/dotnet-framework) (an
older Windows-based .NET implementation), except `.NET Framework 3.5`.

Some instrumentations and instrumentation libraries referenced by the
distribution don't support both .NET and .NET Framework, but only one of them.
For details, refer to the list of [supported instrumentations](#supported-instrumentations).

### Install the full package with all available instrumentations

For installing the distribution with the full set of dependencies, add a
reference to the [`Grafana.OpenTelemetry`](https://www.nuget.org/packages/Grafana.OpenTelemetry)
package to your project.

```sh
dotnet add package --prerelease Grafana.OpenTelemetry
```

The list of [supported instrumentations](#supported-instrumentations)
specifies what instrumentations are included in the full package.

### Install the base package

For installing the distribution with a minimal set of dependencies, add a
reference to the [`Grafana.OpenTelemetry.Base`](https://www.nuget.org/packages/Grafana.OpenTelemetry.Base)
package to your project.

```sh
dotnet add package --prerelease Grafana.OpenTelemetry.Base
```

The list of [supported instrumentations](#supported-instrumentations)
specifies what instrumentations are included in the base package.

## Configuration

* [Configuring metrics](#configuring-metrics)
* [Configuring logs](#configuring-logs)
* [Configuring traces](#configuring-traces)
* [Exporter configuration](#exporter-configuration)
  * [Sending to an agent or collector via OTLP](#sending-to-an-agent-or-collector-via-otlp)
  * [Sending data directly to Grafana Cloud via OTLP](#sending-data-directly-to-grafana-cloud-via-otlp)
* [Instrumentation configuration](#instrumentation-configuration)
  * [Disabling instrumentations](#disabling-instrumentations)
  * [Adding instrumentations](#adding-instrumentations)
  * [Extra steps to activate specific instrumentations](#extra-steps-to-activate-specific-instrumentations)
    * [ASP.NET (`AspNet`)](#aspnet-aspnet)
    * [OWIN (`Owin`)](#owin-owin)
* [Supported environment variables](#supported-environment-variables)

### Configuring metrics

The distribution can be initialized for metrics by calling the `UseGrafana`
extension method on the `MeterProviderBuilder`.

```csharp
using var tracerProvider = Sdk.CreateMeterProviderBuilder()
    .UseGrafana()
    .Build();
```

### Configuring logs

The distribution can be initialized for logs by calling the `UseGrafana`
extension method on the `OpenTelemetryLoggerOptionsExtensions`.

```csharp
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddOpenTelemetry(logging =>
    {
        logging.UseGrafana();
    });
});

var logger = loggerFactory.CreateLogger<Program>();
```

### Configuring traces

The distribution can be initialized for traces by calling the `UseGrafana`
extension method on the `TracerProviderBuilder`.

```csharp
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .UseGrafana()
    .Build();
```

### Exporter configuration

#### Sending to an agent or collector via OTLP

By default, telemetry data will be sent to a Grafana agent or an OTel collector
that runs locally via the [default OTLP port for
HTTP/Protobuf](https://opentelemetry.io/docs/specs/otel/protocol/exporter/)
(4318).

```csharp
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .UseGrafana()
    .Build();
```

The agent or collector address and protocol can be customized by initializing
an `AgentOtlpExporter` and setting the attributes `Endpoint` and `Protocol`.

```csharp
using var tracerProvider = Sdk.CreatetracerProviderBuilder()
    .UseGrafana(config => {
       var agentExporter = new AgentOtlpExporter();

       agentExporter.Endpoint = new Uri("http://grafana-agent:4318");

       config.ExporterSettings = agentExporter;
    })
    .Build();
```

Alternatively, the OTLP endpoint and protocol can be customized via default
OpenTelemetry environment variables `OTEL_EXPORTER_OTLP_ENDPOINT` and
`OTEL_EXPORTER_OTLP_PROTOCOL`.

```sh
export OTEL_EXPORTER_OTLP_ENDPOINT=http://grafana-agent:4318
export OTEL_EXPORTER_OTLP_PROTOCOL=http/protobuf
```

For further details on environment variables, see the  [OTLP exporter
documentation](https://opentelemetry.io/docs/specs/otel/protocol/exporter/#endpoint-urls-for-otlphttp).

#### Sending data directly to Grafana Cloud via OTLP

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

For details on how to obtain those values, refer to [Send data using
OpenTelemetry
Protocol](https://grafana.com/docs/grafana-cloud/monitor-infrastructure/otlp/send-data-otlp/).

Alternatively, these values can be set via the environment variables
`GRAFANA_OTLP_CLOUD_ZONE`, `GRAFANA_OTLP_CLOUD_INSTANCE_ID`, and
`GRAFANA_OTLP_CLOUD_API_KEY`.

```sh
export GRAFANA_OTLP_CLOUD_ZONE=prod-us-east-0
export GRAFANA_OTLP_CLOUD_INSTANCE_ID=123456
export GRAFANA_OTLP_CLOUD_API_KEY=a-secret-token
```

### Instrumentation configuration

#### Disabling instrumentations

By default, all supported instrumentation libraries except `AWSLambda` are
enabled. Instrumentation libraries can be disabled by removing them from the
`Instrumentations` set in the configuration:

```csharp
using var tracerProvider = Sdk.CreateMeterProviderBuilder()
    .UseGrafana(config =>
    {
        config.Instrumentations.Remove(Instrumentation.Process);
        config.Instrumentations.Remove(Instrumentation.NetRuntime);
    })
    .Build();
```

Alternatively, instrumentation libraries can be disabled by the environment
variable `GRAFANA_DOTNET_DISABLE_INSTRUMENTATIONS`. This variable can be
populated with a comma-separated list of instrumentation library identifiers
from the table above:

```sh
export GRAFANA_DOTNET_DISABLE_INSTRUMENTATIONS="Process,NetRuntime"
```

#### Adding instrumentations

Instrumentations not included in the distribution can easily be added by
extension methods on the tracer and meter provider.

For example, it is desired to use the `AspNetCore` instrumentation in
combination with the [base package](#install-the-base-package) (which doesn't
include the `AspNetCore` package), you can install the `AspNetCore`
instrumentation library along with the base package.

```sh
dotnet add package --prerelease Grafana.OpenTelemetry.Base
dotnet add package --prerelease OpenTelemetry.Instrumentation.AspNetCore
```

Then, the `AspNetCore` instrumentation can be enabled via the [`AddAspNetCoreInstrumentation`](https://github.com/open-telemetry/opentelemetry-dotnet/tree/main/src/OpenTelemetry.Instrumentation.AspNetCore#step-2-enable-aspnet-core-instrumentation-at-application-startup)
extension method, alongside the `UseGrafana` method.

```csharp
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .UseGrafana()
    .AddAspNetCoreInstrumentation()
    .Build();
```

This way, any other instrumentation library can be added according the
documentation provided with it.

#### Extra steps to activate specific instrumentations

##### ASP.NET (`AspNet`)

To active ASP.NET instrumentation, it is necessary to add an additional HTTP
module `OpenTelemetry.Instrumentation.AspNet.TelemetryHttpModule` to the web
server. This module is shipped as dependency of the
`OpenTelemetry.Instrumentation.AspNet` package. When using the IIS web server,
the following changes to `Web.config` are required.

```xml
<system.webServer>
  <modules>
    <add name="TelemetryHttpModule"
         type="OpenTelemetry.Instrumentation.AspNet.TelemetryHttpModule,
               OpenTelemetry.Instrumentation.AspNet.TelemetryHttpModule"
         preCondition="integratedMode,managedHandler" />
  </modules>
</system.webServer>
```

Refer to the [upstream
documentation](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Instrumentation.AspNet#step-2-modify-webconfig)
for further details.

##### OWIN (`Owin`)

To activate OWIN instrumentation, it is necessary to register the OpenTelemetry
middleware by calling `UseOpenTelemetry` on the `IAppBuilder`. This should be
done before registering any other middleware.

```csharp
using var host = WebApp.Start(
    "http://localhost:9000",
    appBuilder =>
    {
        appBuilder.UseOpenTelemetry();
    });
```

Refer to the [upstream
documentation](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Instrumentation.Owin#step-2-configure-owin-middleware)
for further details.

### Supported environment variables

| Variable                                  | Example value        | Description |
| ----------------------------------------- |   ------------------ | ----------- |
| `GRAFANA_DOTNET_DISABLE_INSTRUMENTATIONS` | "Process,NetRuntime" | A comma-separated list of instrumentations to disable. |
| `GRAFANA_OTLP_CLOUD_ZONE`                 | "prod-us-east-0"     | Zone of the Grafana Cloud stack to send data to. |
| `GRAFANA_OTLP_CLOUD_INSTANCE_ID`          | "123456"             | Instance id of the Grafana Cloud stack to send data to. |
| `GRAFANA_OTLP_CLOUD_API_KEY`              |                      | API key of the Grafana Cloud Stack to send data to. |

## Supported instrumentations

At the moment, the following instrumentations are included in the distribution
packages with [full](#install-the-full-package-with-all-available-instrumentations)
and [minimal](#install-the-base-package) dependencies:

| Identifier            | Full               | Base               | Library name |
| --------------------- | ------------------ | ------------------ | ------------ |
| `AspNet`              | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.AspNet](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.AspNet) |
| `AspNetCore`          | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.AspNetCore](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.AspNetCore) |
| `AWS`                 | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.AWS](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.AWS) |
| `AWSLambda`           | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.AWSLambda](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.AWSLambda) |
| `Cassandra`           | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.Cassandra](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Cassandra) |
| `ElasticsearchClient` | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.ElasticsearchClient](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.ElasticsearchClient) |
| `EntityFrameworkCore` | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.EntityFrameworkCore](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.EntityFrameworkCore) |
| `GrpcNetClient`       | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Instrumentation.GrpcNetClient](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.GrpcNetClient) |
| `Hangfire`            | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.Hangfire](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Hangfire) |
| `HttpClient`          | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Instrumentation.Http](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Http) |
| `NetRuntime`          | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Instrumentation.Runtime](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Runtime) |
| `Process`             | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Instrumentation.Process](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Process) |
| `MySqlData`           | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Instrumentation.MySqlData](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.MySqlData) |
|                       | :heavy_check_mark: |                    | [MySql.Data.OpenTelemetry](https://www.nuget.org/packages/MySql.Data.OpenTelemetry) |
| `Owin`                | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.Owin](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Owin) |
| `Quartz`              | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.Quartz](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Quartz) |
| `SqlClient`           | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Instrumentation.SqlClient](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.SqlClient) |
| `StackExchangeRedis`  | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.StackExchangeRedis](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.StackExchangeRedis) |
| `Wcf`                 | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.Wcf](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Wcf) |

The `AWSLambda` instrumentation is included, but needs to be explicitly
activated, as activating it in non-AWS scenarios causes errors.

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
  and join the `#application-observability` channel.
* Ask questions on the [Discussions page](https://github.com/grafana/grafana-opentelemetry-dotnet/discussions).
* [File an issue](https://github.com/grafana/grafana-opentelemetry-dotnet/issues/new)
  for bugs, issues, and feature suggestions.
