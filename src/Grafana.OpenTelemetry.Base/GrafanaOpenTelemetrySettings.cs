//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

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

        // As a workaround, a static random service instance id is initialized and used as default.
        // This is to avoid different instance ids to be created by provider builders for different
        // signals.
        //
        // This can be removed once the related issue is resolved:
        // https://github.com/open-telemetry/opentelemetry-dotnet/issues/4871
        internal static string DefaultServiceInstanceId = Guid.NewGuid().ToString();

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
        public string ServiceName { get; set; } = Assembly.GetEntryAssembly()?.GetName().Name ?? System.Diagnostics.Process.GetCurrentProcess().ProcessName;

        /// <summary>
        /// Gets or sets the version of the service to be instrumented.
        ///
        /// This corresponds to the `service.version` resource attribute.
        /// </summary>
        public string ServiceVersion { get; set; } = Assembly.GetEntryAssembly()?.GetName().Version.ToString() ?? "unknown";

        /// <summary>
        /// Gets or sets the string id of the service instance.
        ///
        /// This corresponds to the `service.instance.id` resource attribute.
        /// </summary>
        public string ServiceInstanceId { get; set; } = DefaultServiceInstanceId;

        /// <summary>
        /// Gets or sets the name of the deployment environment ("staging" or "production").
        /// </summary>
        public string DeploymentEnvironment { get; set; } = "production";

        /// <summary>
        /// Gets a dictionary of custom resource attributes.
        /// </summary>
        public IDictionary<string, object> ResourceAttributes { get; } = new Dictionary<string, object>();

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


            // Activating AWSLambda instrumentation by default causes the provider builder to bail in case certain
            // Lambda-specific environment variables are missing.
            //
            // De-activate it until the related issue is resolved: https://github.com/grafana/app-o11y/issues/378
            Instrumentations.Remove(Instrumentation.AWSLambda);

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

            // Set deployment environment from known environment variables.
            foreach (var envVarName in new string[] { "DOTNET_ENVIRONMENT", "ASPNETCORE_ENVIRONMENT" })
            {
                var deploymentEnvironment = Environment.GetEnvironmentVariable(envVarName);

                if (deploymentEnvironment != null)
                {
                    DeploymentEnvironment = deploymentEnvironment.ToLower();
                }
            }
        }
    }
}
