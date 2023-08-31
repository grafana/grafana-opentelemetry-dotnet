using System;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry;

/// <summary>
/// Tracing-related extension provided by the OpenTelemetry .NET distribution for Grafana.
/// </summary>
public static class TracerProviderBuilderExtensions
{
    /// <summary>
    /// Sets up tracing with the OpenTelemetry .NET distribution for Grafana.
    /// </summary>
    /// <param name="builder">A <see cref="TracerProviderBuilder"/></param>
    /// <param name="configure">A callback for customizing default Grafana OpenTelemetry settings</param>
    /// <returns>A modified <see cref="TracerProviderBuilder"/> </returns>
    public static TracerProviderBuilder UseGrafana(this TracerProviderBuilder builder, Action<GrafanaOpenTelemetrySettings>? configure = default)
    {
        GrafanaOpenTelemetrySettings settings = new();

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
