//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal sealed class EntityFrameworkCoreInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.EntityFrameworkCore;

#if NET8_0_OR_GREATER
        [System.Diagnostics.CodeAnalysis.DynamicDependency(System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "OpenTelemetry.Trace.TracerProviderBuilderExtensions", "OpenTelemetry.Instrumentation.EntityFrameworkCore")]
        [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage(TrimWarnings.Category, TrimWarnings.CheckId, Justification = TrimWarnings.Justification)]
#endif
        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.EntityFrameworkCore",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddEntityFrameworkCoreInstrumentation",
                [builder]);
        }
    }
}
