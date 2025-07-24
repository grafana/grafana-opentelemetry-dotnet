//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using Microsoft.Extensions.Configuration;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    /// <summary>
    /// Settings for exporting telemetry directly to Grafana Alloy via OTLP.
    /// </summary>
    [Obsolete("This class is obsolete. Use OtlpExporter instead.")]
    public class CloudOtlpExporter : ExporterSettings
    {
        internal const string ZoneEnvVarName = "GRAFANA_CLOUD_ZONE";
        internal const string InstanceIdEnvVarName = "GRAFANA_CLOUD_INSTANCE_ID";
        internal const string ApiKeyEnvVarName = "GRAFANA_CLOUD_API_KEY";

        /// <summary>
        /// Gets or sets the zone of the Grafana Cloud stack.
        /// </summary>
        public string Zone { get; }

        /// <summary>
        /// Gets or sets the instance id of the Grafana Cloud stack.
        /// </summary>
        public string InstanceId { get; }

        /// <summary>
        /// Gets or sets the API key for sending data to the Grafana Cloud stack.
        /// </summary>
        public string ApiKey { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="CloudOtlpExporter"/>.
        ///
        /// Parameters are set from environment variables.
        /// </summary>
        public CloudOtlpExporter()
          : this(new ConfigurationBuilder().AddEnvironmentVariables().Build())
        { }

        /// <summary>
        /// Initializes a new instance of <see cref="CloudOtlpExporter"/> with given
        /// parameters.
        /// </summary>
        /// <param name="zone">The zone of the Grafana cloud stack</param>
        /// <param name="instanceId">The instance id of the Grafana cloud stack</param>
        /// <param name="apiKey">The API token for sending data to the Grafana cloud stack</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CloudOtlpExporter(string zone, string instanceId, string apiKey)
        {
            this.Zone = zone ?? throw new ArgumentNullException(nameof(zone));
            this.InstanceId = instanceId ?? throw new ArgumentNullException(nameof(instanceId));
            this.ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        }

        internal CloudOtlpExporter(IConfiguration configuration)
            : this(configuration[ZoneEnvVarName], configuration[InstanceIdEnvVarName], configuration[ApiKeyEnvVarName])
        { }

        /// <inheritdoc/>
        internal override void Apply(TracerProviderBuilder builder)
        {
            if (!EnableTraces)
            {
                return;
            }

            builder.AddOtlpExporter(config =>
            {
                var configurationHelper = new GrafanaCloudConfigurationHelper(Zone, InstanceId, ApiKey);

                config.Endpoint = configurationHelper.OtlpEndpointTraces;
                config.Headers = configurationHelper.OtlpAuthorizationHeader;
                config.Protocol = OtlpExportProtocol.HttpProtobuf;
            });
        }

        /// <inheritdoc/>
        internal override void Apply(MeterProviderBuilder builder)
        {
            if (!EnableMetrics)
            {
                return;
            }

            builder.AddOtlpExporter(config =>
            {
                var configurationHelper = new GrafanaCloudConfigurationHelper(Zone, InstanceId, ApiKey);

                config.Endpoint = configurationHelper.OtlpEndpointMetrics;
                config.Headers = configurationHelper.OtlpAuthorizationHeader;
                config.Protocol = OtlpExportProtocol.HttpProtobuf;
            });
        }

        /// <inheritdoc/>
        internal override void Apply(OpenTelemetryLoggerOptions options)
        {
            if (!EnableLogs)
            {
                return;
            }

            options.AddOtlpExporter(config =>
            {
                var configurationHelper = new GrafanaCloudConfigurationHelper(Zone, InstanceId, ApiKey);

                config.Endpoint = configurationHelper.OtlpEndpointLogs;
                config.Headers = configurationHelper.OtlpAuthorizationHeader;
                config.Protocol = OtlpExportProtocol.HttpProtobuf;
            });
        }
    }
}
