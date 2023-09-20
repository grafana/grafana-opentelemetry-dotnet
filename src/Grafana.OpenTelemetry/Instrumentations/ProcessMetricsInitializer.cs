using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class ProcessMetricsInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.Process;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {}

        protected override void InitializeMetrics(MeterProviderBuilder builder)
        {
            builder.AddProcessInstrumentation();
        }
    }
}
