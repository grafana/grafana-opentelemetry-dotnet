//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
#if NET8_0_OR_GREATER
    [System.Diagnostics.CodeAnalysis.RequiresUnreferencedCode("Types might be removed")]
#endif
    internal sealed class OwinInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.Owin;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.Owin",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddOwinInstrumentation",
                [builder, null]);
        }
    }
}
