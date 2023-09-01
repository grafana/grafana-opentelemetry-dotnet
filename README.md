# Grafana distribution of OpenTelemetry .NET instrumentation

This is a pre-configured and pre-packaged bundle of OpenTelemetry .NET components, optimized for the Grafana Stack.

## Getting Started

### Step 1: Install package

### Step 2: Enable the Grafana distribution at application startup

The `UseGrafana` extension method on the `TracerProviderBuilder` or the `MetricProviderBuilder` can be used to set up the Grafana distribution. By default, telemetry data will be sent to a Grafana agent or an OTel collector that runs locally and listens to default OTLP ports:

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

Given the zone, instance id, and API token, telemetry data can be sent directly to the Grafana Cloud without involving an agent or collector:

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
