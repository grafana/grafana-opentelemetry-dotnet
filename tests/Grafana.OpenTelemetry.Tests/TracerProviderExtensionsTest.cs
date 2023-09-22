using Grafana.OpenTelemetry;
using OpenTelemetry;
using OpenTelemetry.Trace;
using Xunit;

namespace Grafana.OpenTelemetry.Tests
{
    public class TracerProviderExtensionsTest
    {
        [Fact]
        public void EnableDefaultInstrumentations()
        {
            Sdk
                .CreateTracerProviderBuilder()
                .UseGrafana()
                .Build();
        }
    }
}
