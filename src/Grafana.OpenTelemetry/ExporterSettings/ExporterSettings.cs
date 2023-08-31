using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry;

/// <summary>
/// All OpenTelemetry exporter settings supported by Grafana need to derive from this class.
/// </summary>
public abstract class ExporterSettings
{
    /// <summary>
    /// Gets or sets whether traces should be sent to Grafana.
    /// </summary>
    public bool EnableTraces { get; set; } = true;

    /// <summary>
    /// Gets or sets whether metrics should be sent to Grafana.
    /// </summary>
    public bool EnableMetrics { get; set; } = true;

    /// <summary>
    /// Gets or sets whether logs should be sent to Grafana.
    /// </summary>
    public bool EnableLogs { get; set; } = true;

    /// <summary>
    /// Applies the exporter settings by initializing an exporter on the
    /// given <see cref="TracerProviderBuilder"/>.
    /// </summary>
    /// <param name="builder">A <see cref="TracerProviderBuilder"/></param>
    internal abstract void Apply(TracerProviderBuilder builder);

    /// <summary>
    /// Applies the exporter settings by initializing an exporter on the
    /// given <see cref="MeterProviderBuilder"/>.
    /// </summary>
    /// <param name="builder">A <see cref="MeterProviderBuilder"/></param>
    internal abstract void Apply(MeterProviderBuilder builder);

    /// <summary>
    /// Applies the exporter settings by initializing an exporter on the
    /// given <see cref="ILoggingBuilder"/>.
    /// </summary>
    /// <param name="builder">A <see cref="ILoggingBuilder"/></param>
    internal abstract void Apply(ILoggingBuilder builder);
}
