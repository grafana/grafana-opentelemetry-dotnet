using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class GrpcNetClientInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.GrpcNetClient;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            builder.AddGrpcClientInstrumentation();
        }
    }
}
