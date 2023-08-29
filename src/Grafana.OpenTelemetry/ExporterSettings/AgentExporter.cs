using OpenTelemetry.Exporter;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;

namespace Grafana.OpenTelemetry;

public class AgentExporter : ExporterSettings
{
  public void Apply(TracerProviderBuilder builder)
  {
    builder.AddOtlpExporter();
  }

  public void Apply(MeterProviderBuilder builder)
  {
    builder.AddOtlpExporter();
  }
}
