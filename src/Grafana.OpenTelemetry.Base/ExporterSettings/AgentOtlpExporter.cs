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
    /// Settings for exporting telemetry to a Grafana Alloy or collector.
    /// </summary>
    public class AgentOtlpExporter : ExporterSettings
    {
        /// <summary>
        /// Gets or sets the address of the Grafana Alloy or collector. If not set, the OpenTelemetry
        /// default is used (`http://localhost:4817` for http/protobuf, and `http://localhost:4818`
        /// for grpc).
        /// </summary>
        public Uri Endpoint { get; set; }

        /// <summary>
        /// The OTLP protocol to be used for exporting telemetry data.
        /// </summary>
        public OtlpExportProtocol Protocol { get; set; }

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
        override internal void Apply(OpenTelemetryLoggerOptions options)
        {
            if (EnableLogs == false)
            {
                return;
            }

            options.AddOtlpExporter(config => ApplyToConfig(config));
        }

        private void ApplyToConfig(OtlpExporterOptions options)
        {
            if (Endpoint != null)
            {
                options.Endpoint = Endpoint;
            }

            if (Protocol != default)
            {
                options.Protocol = (OtlpExportProtocol)Protocol;
            }
        }
    }
}
