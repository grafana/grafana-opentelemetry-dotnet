using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Grafana.OpenTelemetry
{
    /// <summary>
    /// Settings for configuring the OpenTelemetry .NET distribution for Grafana.
    /// </summary>
    public class GrafanaOpenTelemetrySettings
    {
        internal const string DisableInstrumentationsEnvVarName = "GRAFANA_DOTNET_DISABLE_INSTRUMENTATIONS";
        internal const string ServiceNameEnvVarName = "OTEL_SERVICE_NAME";

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
        /// Gets or sets the logical name of the service to be instrumented.
        ///
        /// This corresponds to the `service.name` resource attribute.
        /// </summary>
        public string ServiceName { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>
        /// Gets or sets the version of the service to be instrumented.
        ///
        /// This corresponds to the `service.version` resource attribute.
        /// </summary>
        public string ServiceVersion { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Gets or sets the string id of the service instance.
        ///
        /// This corresponds to the `service.instance.id` resource attribute.
        /// </summary>
        public string ServiceInstanceId { get; set; } = null;

        /// <summary>
        /// Gets or sets the name of the deployment environment ("staging" or "production").
        /// </summary>
        public string DeploymentEnvironment { get; set; } = "production";

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

            if (!string.IsNullOrEmpty(disableInstrumentations))
            {
                foreach (var instrumentationStr in disableInstrumentations.Split(new char[] { ',', ':' }))
                {
                    if (Enum.TryParse<Instrumentation>(instrumentationStr, out var instrumentation))
                    {
                        Instrumentations.Remove(instrumentation);
                    }
                }
            }

            var serviceName = configuration[ServiceNameEnvVarName];

            if (!string.IsNullOrEmpty(serviceName))
            {
                ServiceName = serviceName;
            }
        }
    }
}
