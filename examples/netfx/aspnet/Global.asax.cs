using System.Web.Http;
using Grafana.OpenTelemetry;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace AspNetExample
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private MeterProvider meterProvider;
        private TracerProvider tracerProvider;

        protected void Application_Start()
        {
            meterProvider = Sdk.CreateMeterProviderBuilder()
                .UseGrafana()
                .Build();
            tracerProvider = Sdk.CreateTracerProviderBuilder()
                .UseGrafana()
                .Build();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_End()
        {
            meterProvider?.Dispose();
            tracerProvider?.Dispose();
        }
    }
}
