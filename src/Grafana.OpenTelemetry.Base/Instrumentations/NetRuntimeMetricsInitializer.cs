using OpenTelemetry.Metrics;

namespace Grafana.OpenTelemetry
{
    internal class NetRuntimeMetricsInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.NetRuntime;

        protected override void InitializeMetrics(MeterProviderBuilder builder)
        {
            builder.AddRuntimeInstrumentation();
        }
    }
}
