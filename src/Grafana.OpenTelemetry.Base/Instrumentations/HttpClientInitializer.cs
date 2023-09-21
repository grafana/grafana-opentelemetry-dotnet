using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class HttpClientInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.HttpClient;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            builder.AddHttpClientInstrumentation();
        }

        protected override void InitializeMetrics(MeterProviderBuilder builder)
        {
            builder.AddHttpClientInstrumentation();
        }
    }
}
