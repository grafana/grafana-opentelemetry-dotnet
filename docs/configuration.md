# Configuring the OpenTelemetry Grafana .NET distributiont

## Configuring metrics

### ASP.NET
## Configuring logs 

### ASP.NET
## Configuring traces

### ASP.NET

## Exporter configuration

### Sending data directly to Grafana Cloud via OTLP

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

## Instrumentation library configuration


### Initializing additional instrumentation libraries

### Disable certain signals

## Supported environment variables

