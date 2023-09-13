using System;
using System.Collections.Generic;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
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
        public static TracerProviderBuilder UseGrafana(this TracerProviderBuilder builder, Action<GrafanaOpenTelemetrySettings> configure = default)
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

        internal static TracerProviderBuilder AddGrafanaExporter(this TracerProviderBuilder builder, ExporterSettings settings)
        {
            settings?.Apply(builder);

            return builder;
        }

        internal static TracerProviderBuilder AddInstrumentations(this TracerProviderBuilder builder, HashSet<Instrumentation> instrumentations)
        {
            if (instrumentations == null)
            {
                return builder;
            }

            foreach (var instrumentation in instrumentations)
            {
                switch (instrumentation)
                {
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
