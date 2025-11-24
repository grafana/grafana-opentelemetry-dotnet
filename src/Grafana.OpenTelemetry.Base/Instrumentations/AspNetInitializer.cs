//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal sealed class AspNetInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.AspNet;

#if NET8_0_OR_GREATER
        [System.Diagnostics.CodeAnalysis.DynamicDependency(System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "OpenTelemetry.Trace.TracerProviderBuilderExtensions", "OpenTelemetry.Instrumentation.AspNet")]
        [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage(TrimWarnings.Category, TrimWarnings.CheckId, Justification = TrimWarnings.Justification)]
#endif
        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.AspNet",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddAspNetInstrumentation",
                [builder]);
        }

#if NET8_0_OR_GREATER
        [System.Diagnostics.CodeAnalysis.DynamicDependency(System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "OpenTelemetry.Metrics.MeterProviderBuilderExtensions", "OpenTelemetry.Instrumentation.AspNet")]
        [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage(TrimWarnings.Category, TrimWarnings.CheckId, Justification = TrimWarnings.Justification)]
#endif
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
