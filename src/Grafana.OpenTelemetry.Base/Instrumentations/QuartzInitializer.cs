//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class QuartzInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.Quartz;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.Quartz",
                "OpenTelemetry.Trace.TraceProviderBuilderExtensions",
                "AddQuartzInstrumentation",
                new object[] { builder });
        }
    }
}
