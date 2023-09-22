using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class MySqlDataInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.MySqlData;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            // MySQL.Data.OpenTelemetry
            builder.AddConnectorNet();

            // OpenTelemetry.Instrumentation.MySqlData
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.MySqlData",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddMySqlDataInstrumentation",
                new object[] { builder });
        }
    }
}
