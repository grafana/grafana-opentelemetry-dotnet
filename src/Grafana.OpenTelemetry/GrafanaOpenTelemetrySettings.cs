namespace Grafana.OpenTelemetry;

/// <summary>
/// Settings for configuring the OpenTelemetry .NET distribution for Grafana.
/// </summary>
public class GrafanaOpenTelemetrySettings
{
    /// <summary>
    /// Gets or sets the exporter settings for sending telemetry data to Grafana.
    ///
    /// By default, data is sent locally to the default OTLP port.
    ///
    /// If set to `null`, no exporter will be initialized by the OpenTelemetry .NET
    /// distribution for Grafana.
    /// </summary>
    public ExporterSettings? ExporterSettings { get; set; } = new AgentOtlpExporter();
}
