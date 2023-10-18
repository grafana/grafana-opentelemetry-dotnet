//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class HangfireInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.Hangfire;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.Hangfire",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddHangfireInstrumentation",
                new object[] { builder });
        }
    }
}
