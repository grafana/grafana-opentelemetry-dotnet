//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
    /// <summary>
    /// Logging-related extension provided by the OpenTelemetry .NET distribution for Grafana.
    /// </summary>
    public static class OpenTelemetryLoggerOptionsExtensions
    {
        /// <summary>
        /// Sets up an <see cref="ILoggerFactory"/> with the OpenTelemetry .NET distribution for Grafana.
        /// </summary>
        /// <param name="options"><see cref="OpenTelemetryLoggerOptions"/></param>
        /// <param name="configure">A callback for customizing default Grafana OpenTelemetry settings</param>
        /// <returns>A modified <see cref="OpenTelemetryLoggerOptions"/> object </returns>
        public static OpenTelemetryLoggerOptions UseGrafana(this OpenTelemetryLoggerOptions options, Action<GrafanaOpenTelemetrySettings> configure = default)
        {
            GrafanaOpenTelemetrySettings settings = new GrafanaOpenTelemetrySettings();

            configure?.Invoke(settings);

            GrafanaOpenTelemetryEventSource.Log.InitializeDistribution(settings);

            // Default to using stable HTTP semantic conventions
            if (Environment.GetEnvironmentVariable("OTEL_SEMCONV_STABILITY_OPT_IN") == null)
            {
                Environment.SetEnvironmentVariable("OTEL_SEMCONV_STABILITY_OPT_IN", "http");
            }

            var resourceBuilder = ResourceBuilder
                .CreateDefault()
                .AddGrafanaResource(settings);

            return options
                .AddGrafanaExporter(settings?.ExporterSettings)
                .SetResourceBuilder(resourceBuilder);
        }

        internal static OpenTelemetryLoggerOptions AddGrafanaExporter(this OpenTelemetryLoggerOptions options, ExporterSettings settings)
        {
            settings?.Apply(options);

            return options;
        }
    }
}
