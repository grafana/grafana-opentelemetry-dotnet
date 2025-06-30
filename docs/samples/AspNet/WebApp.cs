#region snippet dotnet-configure-otel-netfx
// .NET Framework
using Grafana.OpenTelemetry;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace DemoApplication
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private TracerProvider tracerProvider;
        private MeterProvider meterProvider;

        protected void Application_Start()
        {
            // MVC, Web API, etc. configuration
            tracerProvider = Sdk.CreateTracerProviderBuilder()
                .UseGrafana()
                .Build();

            meterProvider = Sdk.CreateMeterProviderBuilder()
                .UseGrafana()
                .Build();
        }

        protected void Application_End()
        {
            tracerProvider?.Dispose();
            meterProvider?.Dispose();
        }
    }
}
#endregion snippet dotnet-configure-otel-netfx
