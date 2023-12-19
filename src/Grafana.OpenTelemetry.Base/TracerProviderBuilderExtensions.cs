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
        public static TracerProviderBuilder UseGrafana(this TracerProviderBuilder builder, Action<GrafanaOpenTelemetrySettings> configure = default)
        {
            GrafanaOpenTelemetrySettings settings = new GrafanaOpenTelemetrySettings();

            if (configure != null)
            {
                configure?.Invoke(settings);
            }

            GrafanaOpenTelemetryEventSource.Log.InitializeDistribution(settings);

            // Default to using experimental gRPC instrumentation
            if (Environment.GetEnvironmentVariable("OTEL_DOTNET_EXPERIMENTAL_ASPNETCORE_ENABLE_GRPC_INSTRUMENTATION") == null)
            {
                Environment.SetEnvironmentVariable("OTEL_DOTNET_EXPERIMENTAL_ASPNETCORE_ENABLE_GRPC_INSTRUMENTATION", "true");
            }

            return builder
                .AddGrafanaExporter(settings?.ExporterSettings)
                .AddInstrumentations(settings?.Instrumentations)
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
    }
}
