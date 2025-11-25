//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using System.Collections.Generic;
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
        internal const string DisableResourceDetectorsEnvVarName = "GRAFANA_DOTNET_DISABLE_RESOURCE_DETECTORS";
        internal const string ResourceDetectorsEnvVarName = "GRAFANA_DOTNET_RESOURCE_DETECTORS";
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
        public HashSet<Instrumentation> Instrumentations { get; } =
#if NET
            [.. Enum.GetValues<Instrumentation>()];
#else
            new HashSet<Instrumentation>((Instrumentation[])Enum.GetValues(typeof(Instrumentation)));
#endif

        /// <summary>
        /// Gets the list of resource detectors to be activated.
        ///
        /// By default, all only resource detectors that do not impact application startup are activated.
        /// </summary>
        public HashSet<ResourceDetector> ResourceDetectors { get; } = new HashSet<ResourceDetector>(new ResourceDetector[]
        {
            // Activating the container resource detector by default always populates a `container.id` attribute,
            // even when running in a non-container Linux setting.
            // ResourceDetector.Container,
            ResourceDetector.Host,
            ResourceDetector.Process,
            ResourceDetector.ProcessRuntime,
        });

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
        public string ServiceInstanceId { get; set; }

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
#pragma warning disable CS0618
                ExporterSettings = new CloudOtlpExporter();
#pragma warning restore CS0618
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
            char[] separators = [',', ':'];

            if (!string.IsNullOrEmpty(disableInstrumentations))
            {
                foreach (var instrumentationStr in disableInstrumentations.Split(separators))
                {
                    if (Enum.TryParse<Instrumentation>(instrumentationStr, out var instrumentation))
                    {
                        Instrumentations.Remove(instrumentation);
                    }
                }
            }

            var resourceDetectors = configuration[ResourceDetectorsEnvVarName];

            if (!string.IsNullOrEmpty(resourceDetectors))
            {
                ResourceDetectors.Clear();

                foreach (var resourceDetectorStr in resourceDetectors.Split(separators))
                {
                    if (Enum.TryParse<ResourceDetector>(resourceDetectorStr, out var resourceDetector))
                    {
                        ResourceDetectors.Add(resourceDetector);
                    }
                }
            }

            var disableResourceDetectors = configuration[DisableResourceDetectorsEnvVarName];

            if (!string.IsNullOrEmpty(disableResourceDetectors))
            {
                foreach (var resourceDetectorStr in disableResourceDetectors.Split(separators))
                {
                    if (Enum.TryParse<ResourceDetector>(resourceDetectorStr, out var resourceDetector))
                    {
                        ResourceDetectors.Remove(resourceDetector);
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
