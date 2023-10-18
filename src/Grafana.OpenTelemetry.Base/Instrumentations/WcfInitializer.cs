//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class WcfInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.Wcf;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.Wcf",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddWcfInstrumentation",
                new object[] { builder });
        }
    }
}
