//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    /// <summary>
    /// Settings for exporting telemetry via plain OTLP exporter settings.
    /// </summary>
    public class OtlpExporter : ExporterSettings
    {
        /// <summary>
        /// Gets or sets the target to which the exporter is going to send telemetry.
        /// Must be a valid Uri with scheme (http or https) and host, and
        /// may contain a port and path.
        /// </summary>
        public Uri Endpoint { get; set; }

        /// <summary>
        /// Gets or sets optional headers for the connection. Refer to the <a href="https://github.com/open-telemetry/opentelemetry-specification/blob/main/specification/protocol/exporter.md#specifying-headers-via-environment-variables">specification</a> for information on the expected format for Headers.
        /// </summary>
        public string Headers { get; set; }

        /// <summary>
        /// Gets or sets the the OTLP transport protocol. Supported values: Grpc and HttpProtobuf
        /// </summary>
        public OtlpExportProtocol Protocol { get; set; }

        /// <inheritdoc/>
        internal override void Apply(TracerProviderBuilder builder)
        {
            if (!EnableTraces)
            {
                return;
            }

            builder.AddOtlpExporter(config =>
            {
                config.Endpoint = new Uri($"{Endpoint}/v1/traces");
                config.Headers = Headers;
                config.Protocol = Protocol;
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
                config.Endpoint = new Uri($"{Endpoint}/v1/metrics");
                config.Headers = Headers;
                config.Protocol = Protocol;
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
                config.Endpoint = new Uri($"{Endpoint}/v1/logs");
                config.Headers = Headers;
                config.Protocol = Protocol;
            });
        }
    }
}
