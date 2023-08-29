using System;
using OpenTelemetry.Exporter;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;

namespace Grafana.OpenTelemetry;

public class CloudExporter : ExporterSettings
{
  public string Zone { get; set; } = string.Empty;
  public string InstanceId { get; set; } = string.Empty;
  public string ApiKey { get; set; } = string.Empty;

  public void Apply(TracerProviderBuilder builder)
  {
    builder.AddOtlpExporter(config => {
      config.Endpoint = GetEndpoint();
      config.Headers = GetHeader();
    });
  }

  public void Apply(MeterProviderBuilder builder)
  {
    builder.AddOtlpExporter(config => {
      config.Endpoint = GetEndpoint();
      config.Headers = GetHeader();
    });
  }

  private Uri GetEndpoint() => new Uri($"https://otlp-gateway-{Zone}.grafana.net/otlp");

  private string GetHeader()
  {
    var authorizationValue = System.Convert.ToBase64String(
        System.Text.Encoding.UTF8.GetBytes($"{InstanceId}:{ApiKey}")
    );

    return $"Authorization=Basic {authorizationValue}";
  }
}
