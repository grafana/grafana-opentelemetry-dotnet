//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

#if !NETFRAMEWORK

using System;
using OpenTelemetry;

namespace Grafana.OpenTelemetry
{
    /// <summary>
    /// Extension for the <see cref="IOpenTelemetryBuilder"/> provided by the OpenTelemetry .NET distribution 
    /// for Grafana.
    ///
    /// This is used for easier configuration for ASP.NET Core projects.
    /// </summary>
    public static class OpenTelemetryBuilderExtension
    {
        /// <summary>
        /// Sets up tracing and metrics with the OpenTelemetry .NET distribution for Grafana.
        /// </summary>
        /// <param name="builder">A <see cref="IOpenTelemetryBuilder"/></param>
        /// <param name="configure">A callback for customizing default Grafana OpenTelemetry settings</param>
        /// <returns>A modified <see cref="IOpenTelemetryBuilder"/> </returns>
#if NET8_0_OR_GREATER
        [System.Diagnostics.CodeAnalysis.RequiresUnreferencedCode("The Grafana OpenTelemetry distribution for .NET does not support native AoT.")]
#endif
        public static IOpenTelemetryBuilder UseGrafana(this IOpenTelemetryBuilder builder, Action<GrafanaOpenTelemetrySettings> configure = default)
        {
            return builder
                .WithTracing(tracerProviderBuilder => tracerProviderBuilder.UseGrafana(configure))
                .WithMetrics(metricProviderBuilder => metricProviderBuilder.UseGrafana(configure));
        }
    }
}
#endif
