#region snippet dotnet-configure-otel-console
// .NET 6+ console
using Grafana.OpenTelemetry;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .UseGrafana()
    .AddConsoleExporter()
    .Build();

using var meterProvider = Sdk.CreateMeterProviderBuilder()
    .UseGrafana()
    .AddConsoleExporter()
    .Build();

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddOpenTelemetry(logging =>
    {
        logging.UseGrafana()
               .AddConsoleExporter();
    });
});
#endregion snippet dotnet-configure-otel-console
