using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grafana.OpenTelemetry
{
    /// <summary>
    /// Settings for configuring the OpenTelemetry .NET distribution for Grafana.
    /// </summary>
    public class GrafanaOpenTelemetrySettings
    {
        internal const string DisableInstrumentationsEnvVarName = "GRAFANA_DOTNET_DISABLE_INSTRUMENTATIONS";

        /// <summary>
        /// Gets or sets the exporter settings for sending telemetry data to Grafana.
        ///
        /// By default, this is set to:
        ///  1. j <see cref="CloudOtlpExporter"/> initialized by environment variables, or if
        ///     the environment variables aren't present, it is set to
        ///  2. a <see cref="AgentOtlpExporter"/> with the default OTLP endpoint.
        ///
        /// If set to `null`, no exporter will be initialized by the OpenTelemetry .NET
        /// distribution for Grafana.
        /// </summary>
        public ExporterSettings ExporterSettings { get; set; }

        /// <summary>
        /// Gets the list of instrumentations to be activated.
        ///
        /// By default, all available instrumentations are activated.
        /// </summary>
        public HashSet<Instrumentation> Instrumentations { get; } = new HashSet<Instrumentation>((Instrumentation[])Enum.GetValues(typeof(Instrumentation)));

        /// <summary>
        /// Initializes an instance of <see cref="GrafanaOpenTelemetrySettings"/>.
        /// </summary>
        public GrafanaOpenTelemetrySettings()
          : this(new ConfigurationBuilder().AddEnvironmentVariables().Build())
        { }

        internal GrafanaOpenTelemetrySettings(IConfiguration configuration)
        {
            try
            {
                ExporterSettings = new CloudOtlpExporter();
            }
            catch (Exception)
            {
                ExporterSettings = new AgentOtlpExporter();
            }

            var disableInstrumentations = configuration[DisableInstrumentationsEnvVarName];

            if (disableInstrumentations != null)
            {
                foreach (var instrumentationStr in disableInstrumentations.Split(','))
                {
                    if (Enum.TryParse<Instrumentation>(instrumentationStr, out var instrumentation))
                    {
                        Instrumentations.Remove(instrumentation);
                    }
                }
            }
        }
    }
}
