using System;
using OpenTelemetry.Metrics;

namespace Grafana.OpenTelemetry;

/// <summary>
/// Metrics-related extension provided by the OpenTelemetry .NET distribution for Grafana.
/// </summary>
public static class MeterProviderBuilderExtensions
{
    /// <summary>
    /// Sets up metrics with the OpenTelemetry .NET distribution for Grafana.
    /// </summary>
    /// <param name="builder">A <see cref="MeterProviderBuilder"/></param>
    /// <param name="configure">A callback for customizing default Grafana OpenTelemetry settings</param>
    /// <returns>A modified <see cref="MeterProviderBuilder"/> </returns>
    public static MeterProviderBuilder UseGrafana(this MeterProviderBuilder builder, Action<GrafanaOpenTelemetrySettings>? configure = default)
    {
        GrafanaOpenTelemetrySettings settings = new();

        if (configure != null)
        {
            configure?.Invoke(settings);
        }

        return builder.AddGrafanaExporter(settings?.ExporterSettings);
    }

    internal static MeterProviderBuilder AddGrafanaExporter(this MeterProviderBuilder builder, ExporterSettings? settings)
    {
        settings?.Apply(builder);

        return builder;
    }
}
