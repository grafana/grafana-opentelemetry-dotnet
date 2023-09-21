using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class QuartzInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.Quartz;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.Quartz",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddQuartzInstrumentation",
                new object[] { builder });
        }
    }
}
