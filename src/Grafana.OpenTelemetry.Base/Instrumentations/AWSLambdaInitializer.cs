using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class AWSLambdaInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.AWSLambda;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.AWSLambda",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddAWSLambdaConfigurations",
                new object[] { builder });
        }
    }
}
