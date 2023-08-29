using OpenTelemetry.Exporter;

namespace Grafana.OpenTelemetry;

public class GrafanaOTelSettings
{
  public ExporterSettings? ExporterSettings { get; set; } = new AgentExporter();
}
