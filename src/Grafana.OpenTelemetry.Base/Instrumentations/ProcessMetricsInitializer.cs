using OpenTelemetry.Metrics;

namespace Grafana.OpenTelemetry
{
    internal class ProcessMetricsInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.Process;

        protected override void InitializeMetrics(MeterProviderBuilder builder)
        {
            builder.AddProcessInstrumentation();
        }
    }
}
