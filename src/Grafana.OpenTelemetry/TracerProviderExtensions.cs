using System;
using OpenTelemetry.Trace;
using OpenTelemetry.Exporter.OpenTelemetryProtocol;

namespace Grafana.OpenTelemetry;

public static class TracerProviderBuilderExtensions
{
  public static TracerProviderBuilder UseGrafana(this TracerProviderBuilder builder, Action<GrafanaOTelSettings>? configure = default)
  {
    GrafanaOTelSettings settings = new();

    if (configure != null)
    {
        configure?.Invoke(settings);
    }

    settings.ExporterSettings?.Apply(builder);

    return builder;
  }
}
