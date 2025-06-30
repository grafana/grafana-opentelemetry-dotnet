#region snippet dotnet-configure-otel-webapp
// .NET 6+ ASP.NET Core
using Grafana.OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .WithTracing(configure =>
    {
        configure.UseGrafana()
            .AddConsoleExporter();
    })
    .WithMetrics(configure =>
    {
        configure.UseGrafana()
            .AddConsoleExporter();
    });

builder.Logging.AddOpenTelemetry(options =>
{
    options.UseGrafana()
        .AddConsoleExporter();
});
#endregion snippet dotnet-configure-otel-webapp
