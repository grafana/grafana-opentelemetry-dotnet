using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Configuration;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry;

/// <summary>
/// Settings for exporting telemetry directly to Grafana Agent via OTLP.
/// </summary>
public class CloudOtlpExporter : ExporterSettings
{
    internal const string ZoneEnvVarName = "GRAFANA_OTLP_CLOUD_ZONE";
    internal const string InstanceIdEnvVarName = "GRAFANA_OTLP_CLOUD_INSTANCE_ID";
    internal const string ApiKeyEnvVarName = "GRAFANA_OTLP_CLOUD_API_KEY";

    /// <summary>
    /// Gets or sets the zone of the Grafana Cloud stack.
    /// </summary>
    public string Zone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the instance id of the Grafana Cloud stack.
    /// </summary>
    public string InstanceId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the API key for sending data to the Grafana Cloud stack.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of <see cref="CloudOtlpExporter"/>.
    /// </summary>
    public CloudOtlpExporter()
      : this(new ConfigurationBuilder().AddEnvironmentVariables().Build())
    { }

    internal CloudOtlpExporter(IConfiguration configuration)
    {
        this.Zone = configuration[ZoneEnvVarName] ?? string.Empty;
        this.InstanceId = configuration[InstanceIdEnvVarName] ?? string.Empty;
        this.ApiKey = configuration[ApiKeyEnvVarName] ?? string.Empty;
    }

    /// <inheritdoc/>
    override internal void Apply(TracerProviderBuilder builder)
    {
        if (EnableTraces == false)
        {
            return;
        }

        builder.AddOtlpExporter(config => ApplyToConfig(config));
    }

    /// <inheritdoc/>
    override internal void Apply(MeterProviderBuilder builder)
    {
        if (EnableMetrics == false)
        {
            return;
        }

        builder.AddOtlpExporter(config => ApplyToConfig(config));
    }

    /// <inheritdoc/>
    override internal void Apply(ILoggingBuilder builder)
    {
        if (EnableLogs == false)
        {
            return;
        }

        builder.AddOpenTelemetry(options =>
        {
            options.AddOtlpExporter(config => ApplyToConfig(config));
        });
    }

    private void ApplyToConfig(OtlpExporterOptions options)
    {
        var configurationHelper = new GrafanaCloudConfigurationHelper(Zone, InstanceId, ApiKey);

        options.Endpoint = configurationHelper.OtlpEndpoint;
        options.Headers = configurationHelper.OtlpAuthorizationHeader;
    }
}
