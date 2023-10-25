# Grafana distribution of OpenTelemetry .NET instrumentation

* [About](#about)
* [Getting Started](#getting-started)
* [Installation](#getting-started)
* [Configuration](#configuration)
* [Supported instrumentations](#supported-instrumentations)
* [Troubleshooting](#troubleshooting)

## About

This is a pre-configured and pre-packaged bundle of OpenTelemetry .NET
components, optimized for the Grafana Stack.

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
    .AddAspNetCoreInstrumentation();
```

This way, any other instrumentation library can be added according the
documentation provided with it.

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
