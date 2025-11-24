//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

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
#if NET8_0_OR_GREATER
        [System.Diagnostics.CodeAnalysis.RequiresUnreferencedCode("The Grafana OpenTelemetry distribution for .NET does not support native AoT.")]
#endif
        public static TracerProviderBuilder UseGrafana(this TracerProviderBuilder builder, Action<GrafanaOpenTelemetrySettings> configure = default)
        {
            GrafanaOpenTelemetrySettings settings = new GrafanaOpenTelemetrySettings();

            configure?.Invoke(settings);

            GrafanaOpenTelemetryEventSource.Log.InitializeDistribution(settings);

            // Default to using experimental gRPC instrumentation
            if (Environment.GetEnvironmentVariable("OTEL_DOTNET_EXPERIMENTAL_ASPNETCORE_ENABLE_GRPC_INSTRUMENTATION") == null)
            {
                Environment.SetEnvironmentVariable("OTEL_DOTNET_EXPERIMENTAL_ASPNETCORE_ENABLE_GRPC_INSTRUMENTATION", "true");
            }

            return builder
                .AddGrafanaExporter(settings?.ExporterSettings)
                .AddInstrumentations(settings?.Instrumentations)
                .AddResourceDetectors(settings?.ResourceDetectors)
                .ConfigureResource(resourceBuilder => resourceBuilder.AddGrafanaResource(settings));
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

            foreach (var initializer in InstrumentationInitializer.Initializers)
            {
                if (instrumentations.Contains(initializer.Id))
                {
                    initializer.Initialize(builder);
                }
            }

            return builder;
        }

#if NET8_0_OR_GREATER
        [System.Diagnostics.CodeAnalysis.RequiresUnreferencedCode("Types might be removed")]
#endif
        internal static TracerProviderBuilder AddResourceDetectors(this TracerProviderBuilder builder, HashSet<ResourceDetector> resourceDetectors)
        {
            if (resourceDetectors == null)
            {
                return builder;
            }

            foreach (var initializer in ResourceDetectorInitializer.Initializers)
            {
                if (resourceDetectors.Contains(initializer.Id))
                {
                    initializer.Initialize(builder);
                }
            }

            return builder;
        }
    }
}
