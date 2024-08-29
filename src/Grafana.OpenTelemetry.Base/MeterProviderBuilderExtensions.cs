//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

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

            foreach (var initializer in InstrumentationInitializer.Initializers)
            {
                if (instrumentations.Contains(initializer.Id))
                {
                    initializer.Initialize(builder);
                }
            }

            return builder;
        }

        internal static MeterProviderBuilder AddResourceDetectors(this MeterProviderBuilder builder, HashSet<ResourceDetector> resourceDetectors)
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
