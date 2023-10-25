# <img src="https://github.com/grafana/grafana/blob/main/public/img/grafana_icon.svg" alt="Grafana Icon" width="45" height=""> Grafana distribution of OpenTelemetry .NET instrumentation <img src="https://opentelemetry.io/img/logos/opentelemetry-logo-nav.png" alt="OpenTelemetry Icon" width="45" height="">

* [About](#about)
* [Getting Started](#getting-started)
* [Installation](#getting-started)
* [Supported instrumentation libraries](#supported-instrumentation-libraries)
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
For details, refer to [Supported Instrumentations](supported-instrumentations.md).

### Install the full package with all available instrumentations

For installing the distribution with the full set of dependencies, add a
reference to the [`Grafana.OpenTelemetry`](https://www.nuget.org/packages/Grafana.OpenTelemetry)
package to your project.

```sh
dotnet add package --prerelease Grafana.OpenTelemetry
```

[The list of supported instrumentations](supported-instrumentations.md)
specifies what instrumentations are included in the full package.

### Install the base package 

For installing the distribution with a minimal set of dependencies, add a
reference to the [`Grafana.OpenTelemetry.Base`](https://www.nuget.org/packages/Grafana.OpenTelemetry.Base)
package to your project.

```sh
dotnet add package --prerelease Grafana.OpenTelemetry.Base
```

[The list of supported instrumentations](supported-instrumentations.md)
specifies what instrumentations are included in the base package.

## Supported instrumentation libraries

At the moment, the following instrumentations are included in the distribution
packages with [full](installation.md#install-the-full-package-with-all-available-instrumentations)
and [minimal](installation.md#install-the-base-package) dependencies:

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

By default, all supported instrumentation libraries are enabled. Instrumentation
libraries can be disabled by removing them vom the `Instrumentations` set in the
configuration:

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
