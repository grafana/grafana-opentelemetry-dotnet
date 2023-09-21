using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class AWSInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.AWS;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.AWS",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddAWSInstrumentation",
                new object[] { builder });
        }
    }
}
