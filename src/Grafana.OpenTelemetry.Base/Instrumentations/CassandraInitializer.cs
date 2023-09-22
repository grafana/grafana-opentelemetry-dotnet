using OpenTelemetry.Metrics;

namespace Grafana.OpenTelemetry
{
    internal class CassandraInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.Cassandra;

        protected override void InitializeMetrics(MeterProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.Cassandra",
                "OpenTelemetry.Metrics.MeterProviderBuilderExtensions",
                "AddCassandraInstrumentation",
                new object[] { builder });
        }
    }
}
