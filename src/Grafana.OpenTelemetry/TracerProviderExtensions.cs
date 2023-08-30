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

    return builder.AddGrafanaExporter(settings?.ExporterSettings);
  }

  internal static TracerProviderBuilder AddGrafanaExporter(this TracerProviderBuilder builder, ExporterSettings? settings)
  {
    settings?.Apply(builder);

    return builder;
  }
}
