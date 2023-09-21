using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class AspNetCoreInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.AspNetCore;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.AspNetCore",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddAspNetCoreInstrumentation",
                new object[] { builder });
        }

        protected override void InitializeMetrics(MeterProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.AspNetCore",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddAspNetCoreInstrumentation",
                new object[] { builder });
        }
    }
}
