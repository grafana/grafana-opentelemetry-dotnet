//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class AspNetInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.AspNet;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.AspNet",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddAspNetInstrumentation",
                new object[] { builder });
        }

        protected override void InitializeMetrics(MeterProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.AspNet",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddAspNetInstrumentation",
                new object[] { builder });
        }
    }
}
