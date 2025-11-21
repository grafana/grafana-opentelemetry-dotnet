# Configuring the Grafana OpenTelemetry distribution for .NET

- [Configuring the Grafana OpenTelemetry distribution for .NET](#configuring-the-grafana-opentelemetry-distribution-for-net)
  - [Configuring metrics](#configuring-metrics)
  - [Configuring logs](#configuring-logs)
  - [Configuring traces](#configuring-traces)
  - [ASP.NET Core](#aspnet-core)
  - [Exporter configuration](#exporter-configuration)
    - [Sending to an agent or collector via OTLP](#sending-to-an-agent-or-collector-via-otlp)
    - [Sending data directly to Grafana Cloud via OTLP](#sending-data-directly-to-grafana-cloud-via-otlp)
  - [Instrumentation configuration](#instrumentation-configuration)
    - [Disabling instrumentations](#disabling-instrumentations)
    - [Adding instrumentations not supported by the distribution](#adding-instrumentations-not-supported-by-the-distribution)
  - [Resource detector configuration](#resource-detector-configuration)
    - [Specifying resource detectors](#specifying-resource-detectors)
    - [Disabling resource detectors](#disabling-resource-detectors)
    - [Adding resource detectors not supported by the distribution](#adding-resource-detectors-not-supported-by-the-distribution)
    - [Extra steps to activate specific instrumentations](#extra-steps-to-activate-specific-instrumentations)
      - [ASP.NET (`AspNet`)](#aspnet-aspnet)
      - [OWIN (`Owin`)](#owin-owin)
    - [Customizing resource attributes](#customizing-resource-attributes)
  - [Custom configuration](#custom-configuration)
  - [Supported environment variables](#supported-environment-variables)

## Configuring metrics

The distribution can be initialized for metrics by calling the `UseGrafana`
extension method on the `MeterProviderBuilder`.

```csharp
using var meterProvider = Sdk.CreateMeterProviderBuilder()
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
        config.ExporterSettings = new OtlpExporter
        {
            Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf,
            Endpoint = new Uri("https://otlp-gateway-prod-eu-west-0.grafana.net/otlp"),
            Headers = "Authorization=Basic a-secret-token"
        };
    })
    .Build();
```

Alternatively, these values can be set via the environment variables
`OTEL_EXPORTER_OTLP_PROTOCOL`, `OTEL_EXPORTER_OTLP_ENDPOINT`, and
`OTEL_EXPORTER_OTLP_HEADERS`.

```sh
export OTEL_EXPORTER_OTLP_PROTOCOL="http/protobuf"
export OTEL_EXPORTER_OTLP_ENDPOINT="https://otlp-gateway-prod-eu-west-0.grafana.net/otlp"
export OTEL_EXPORTER_OTLP_HEADERS="Authorization=Basic a-secret-token"
```

For details on how to obtain those values, refer to [Push directly from
applications using the OpenTelemetry
SDKs](https://grafana.com/docs/grafana-cloud/send-data/otlp/send-data-otlp/#push-directly-from-applications-using-the-opentelemetry-sdks).

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
populated with a comma-separated list of
[instrumentation library identifiers](./supported-instrumentations.md):

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

## Resource detector configuration

### Specifying resource detectors

Default resource detectors can be overridden by removing them from the `ResourceDetectors`
set in the configuration and specifying which resource detectors to enable:

```csharp
using var tracerProvider = Sdk.CreateMeterProviderBuilder()
    .UseGrafana(config =>
    {
        config.ResourceDetectors.Clear(ResourceDetector.Host);
        config.ResourceDetectors.Add(ResourceDetector.Process);
    })
    .Build();
```

Alternatively, resource detectors can be specified by the environment
variable `GRAFANA_DOTNET_RESOURCE_DETECTORS`. This variable can be
populated with a comma-separated list of
[resource detector identifiers](./supported-resource-detectors.md):

```sh
export GRAFANA_DOTNET_RESOURCE_DETECTORS="Process"
```

### Disabling resource detectors

By default, `Host`, `Process`, and `ProcessRuntime` resource detectors are enabled.
Resource detectors can be disabled by removing them from the `ResourceDetectors`
set in the configuration:

```csharp
using var tracerProvider = Sdk.CreateMeterProviderBuilder()
    .UseGrafana(config =>
    {
        config.ResourceDetectors.Remove(ResourceDetector.Host);
        config.ResourceDetectors.Remove(ResourceDetector.Process);
    })
    .Build();
```

Alternatively, resource detectors can be disabled by the environment
variable `GRAFANA_DOTNET_DISABLE_RESOURCE_DETECTORS`. This variable can be
populated with a comma-separated list of resource detector identifiers:

```sh
export GRAFANA_DOTNET_DISABLE_RESOURCE_DETECTORS="Host,Process"
```

### Adding resource detectors not supported by the distribution

Resource detectors not included in the distribution can easily be added by
extension methods on the tracer and meter provider.

To enable an unsupported resource detector, call the `ConfigureResource`
extension method, alongside the `UseGrafana` method.

```csharp
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .UseGrafana()
    .ConfigureResource(config => {
        config.AddCustomResourceDetector();
    })
    .Build();
```

This way, any other resource detector library [not supported by the distribution](./supported-resource-detectors.md)
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

### Customizing resource attributes

The type `GrafanaOpenTelemetrySettings` has dedicated fields for setting standard
OpenTelemetry resource attributes for service name, service version, instance
id, and deployment environment. Those fields are populated with reasonable
default values, but can be customized.

```csharp
using var tracerProvider = Sdk.CreateMeterProviderBuilder()
    .UseGrafana(config =>
    {
        config.ServiceName = "service-name";
        config.ServiceVersion= "1.0";
        config.ServiceInstanceId = "instance-34532";
        config.DeploymentEnvironment = "production";
    })
    .Build();
```

Custom resource attributes can be set via the `ResourceAttributes` dictionary:

```csharp
using var tracerProvider = Sdk.CreateMeterProviderBuilder()
    .UseGrafana(config =>
    {
        config.ResourceAttributes["custom.key"] = "custom-value";
    })
    .Build();
```

Alternatively, those attributes can be set via standard OpenTelemetry
environment variables.

```sh
export OTEL_SERVICE_NAME="service-name"
export OTEL_RESOURCE_ATTRIBUTES="service.version=1.0,service.instance.id=instance=34531,deployment.environment=production,custom.key=custom-value"
```

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

| Variable                                   | Example value        | Description                                              |
| ------------------------------------------ | -------------------- | -------------------------------------------------------- |
| `GRAFANA_DOTNET_DISABLE_INSTRUMENTATIONS`  | "Process,NetRuntime" | A comma-separated list of instrumentations to disable.   |
| `GRAFANA_DOTNET_DISABLE_RESOURCE_DETECTORS`| "Host"               | A comma-separated list of resource detectors to disable. |
