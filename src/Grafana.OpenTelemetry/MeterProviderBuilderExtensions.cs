using System;
using System.Collections.Generic;
using OpenTelemetry.Metrics;

namespace Grafana.OpenTelemetry
{
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
        public static MeterProviderBuilder UseGrafana(this MeterProviderBuilder builder, Action<GrafanaOpenTelemetrySettings> configure = default)
        {
            GrafanaOpenTelemetrySettings settings = new GrafanaOpenTelemetrySettings();

            if (configure != null)
            {
                configure?.Invoke(settings);
            }

            return builder
                .AddGrafanaExporter(settings?.ExporterSettings)
                .AddInstrumentations(settings?.Instrumentations);
        }

        internal static MeterProviderBuilder AddGrafanaExporter(this MeterProviderBuilder builder, ExporterSettings settings)
        {
            settings?.Apply(builder);

            return builder;
        }

        internal static MeterProviderBuilder AddInstrumentations(this MeterProviderBuilder builder, HashSet<Instrumentation> instrumentations)
        {
            if (instrumentations == null)
            {
                return builder;
            }

            foreach (var instrumentation in instrumentations)
            {
                switch (instrumentation)
                {
                    case Instrumentation.NetRuntime:
                        {
                            builder.AddRuntimeInstrumentation();
                            break;
                        }
                    case Instrumentation.Process:
                        {
                            builder.AddProcessInstrumentation();
                            break;
                        }
                    case Instrumentation.HttpClient:
                        {
                            builder.AddHttpClientInstrumentation();
                            break;
                        }
                    default:
                        break;
                }
            }

            return builder;
        }
    }
}
