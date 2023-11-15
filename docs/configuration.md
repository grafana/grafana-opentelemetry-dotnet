# Configuring the Grafana OpenTelemetry distribution for .NET

* [Configuring metrics](#configuring-metrics)
* [Configuring logs](#configuring-logs)
* [Configuring traces](#configuring-traces)
* [ASP.NET Core](#aspnet-core)
* [Exporter configuration](#exporter-configuration)
  * [Sending to an agent or collector via OTLP](#sending-to-an-agent-or-collector-via-otlp)
  * [Sending data directly to Grafana Cloud via OTLP](#sending-data-directly-to-grafana-cloud-via-otlp)
* [Instrumentation configuration](#instrumentation-configuration)
  * [Disabling instrumentations](#disabling-instrumentations)
  * [Adding instrumentations not supported by the distribution](#adding-instrumentations-not-supported-by-the-distribution)
  * [Extra steps to activate specific instrumentations](#extra-steps-to-activate-specific-instrumentations)
    * [ASP.NET (`AspNet`)](#aspnet-aspnet)
    * [OWIN (`Owin`)](#owin-owin)
* [Custom configuration](#custom-configuration)
* [Supported environment variables](#supported-environment-variables)

## Configuring metrics

The distribution can be initialized for metrics by calling the `UseGrafana`
extension method on the `MeterProviderBuilder`.

```csharp
using var tracerProvider = Sdk.CreateMeterProviderBuilder()
    .UseGrafana()
    .Build();
```

## Configuring logs

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

## Configuring traces

The distribution can be initialized for traces by calling the `UseGrafana`
extension method on the `TracerProviderBuilder`.

```csharp
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .UseGrafana()
    .Build();
```

## ASP.NET Core

For ASP.NET Core applications, a `UseGrafana` extension method is provided on
the `IServiceCollection`. Invoking this extension method configures both traces
and metrics.

Logging can be set up using the `ILoggingBuilder`, as described in [Configuring logs](#configuring-logs).

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry().UseGrafana();
builder.Logging.AddOpenTelemetry(logging => logging.UseGrafana());
```

## Exporter configuration

### Sending to an agent or collector via OTLP

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

### Sending data directly to Grafana Cloud via OTLP

Given the zone, instance ID, and API token, telemetry data can be sent directly
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

Follow the following steps to obtain the zone, instance id, and an API token:

1. Click **Details** in the **Grafana** section on
   <https://grafana.com/profile/org>.
2. You will see values for **Instance ID** and **Zone** on this page.
3. On the left menu, click on **Security** and then on **API Keys**
4. Obtain a new API token by clicking on **Create API Key** (`MetricsPublisher`
   role).

Alternatively, these values can be set via the environment variables
`GRAFANA_CLOUD_ZONE`, `GRAFANA_CLOUD_INSTANCE_ID`, and
`GRAFANA_CLOUD_API_KEY`.

```sh
export GRAFANA_CLOUD_ZONE=prod-us-east-0
export GRAFANA_CLOUD_INSTANCE_ID=123456
export GRAFANA_CLOUD_API_KEY=a-secret-token
```

## Instrumentation configuration

### Disabling instrumentations

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

### Adding instrumentations not supported by the distribution

Instrumentations not included in the distribution can easily be added by
extension methods on the tracer and meter provider.

For example, if it is desired to use the `EventCounters` instrumentation, which is
not included in the [full package](./installation.md#install-the-full-package-with-all-available-instrumentations),
one install the `EventCounters` instrumentation library along with the base
package.

```sh
dotnet add package --prerelease Grafana.OpenTelemetry.Base
dotnet add package OpenTelemetry.Instrumentation.EventCounters --prerelease
```

Then, the `EventCounters` instrumentation can be enabled via the [`AddEventCountersInstrumentation`](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Instrumentation.EventCounters#step-2-enable-eventcounters-instrumentation)
extension method, alongside the `UseGrafana` method.

```csharp
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .UseGrafana()
    .AddEventCountersInstrumentation(options => {
        options.RefreshIntervalSecs = 1;
        options.AddEventSources("MyEventSource");
    })
    .Build();
```

This way, any other instrumentation library [not supported by the distribution](./supported-instrumentations.md)
can be added according to the documentation provided with it.

### Extra steps to activate specific instrumentations

#### ASP.NET (`AspNet`)

To active ASP.NET instrumentation, it is necessary to add an additional HTTP
module `OpenTelemetry.Instrumentation.AspNet.TelemetryHttpModule` to the web
server. This module is shipped as a dependency of the
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

#### OWIN (`Owin`)

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

## Custom configuration

The distribution is designed to be easily extensible with components that it
doesn't contain. This can be done by invoking additional extension methods on
any of the provider builders in addition to the `UseGrafana` extension method.

The example below initializes the distribution with default settings but
also initializes a console exporter which prints traces to the console.

```csharp
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .UseGrafana()
    .AddConsoleExporter()
    .Build();
```

In the same way, it is possible to add additional instrumentation libraries that
are not contained in the distribution.

## Supported environment variables

| Variable                                  | Example value        | Description |
| ----------------------------------------- |   ------------------ | ----------- |
| `GRAFANA_DOTNET_DISABLE_INSTRUMENTATIONS` | "Process,NetRuntime" | A comma-separated list of instrumentations to disable. |
| `GRAFANA_CLOUD_ZONE`                      | "prod-us-east-0"     | Zone of the Grafana Cloud stack to send data to. |
| `GRAFANA_CLOUD_INSTANCE_ID`               | "123456"             | Instance ID of the Grafana Cloud stack to send data to. |
| `GRAFANA_CLOUD_API_KEY`                   |                      | API key of the Grafana Cloud Stack to send data to. |
