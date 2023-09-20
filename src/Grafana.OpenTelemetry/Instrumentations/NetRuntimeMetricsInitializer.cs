using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class NetRuntimeMetricsInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.NetRuntime;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {}

        protected override void InitializeMetrics(MeterProviderBuilder builder)
        {
            builder.AddRuntimeInstrumentation();
        }
    }
}
