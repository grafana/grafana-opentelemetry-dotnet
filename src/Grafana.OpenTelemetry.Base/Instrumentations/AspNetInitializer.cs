//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
#if NET8_0_OR_GREATER
    [System.Diagnostics.CodeAnalysis.RequiresUnreferencedCode("Types might be removed")]
#endif
    internal sealed class AspNetInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.AspNet;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.AspNet",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddAspNetInstrumentation",
                [builder]);
        }

        protected override void InitializeMetrics(MeterProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.AspNet",
                "OpenTelemetry.Metrics.MeterProviderBuilderExtensions",
                "AddAspNetInstrumentation",
                [builder]);
        }
    }
}
