//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using Grafana.OpenTelemetry;
using Grafana.OpenTelemetry.Sample;
using OpenTelemetry.Logs;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

#region snippet dotnet-configure-otel
builder.Logging.AddOpenTelemetry(builder => builder.UseGrafana());

builder.Services.AddOpenTelemetry()
    .WithMetrics(builder => builder.UseGrafana())
    .WithTracing(builder => builder.UseGrafana().AddConsoleExporter());
#endregion snippet dotnet-configure-otel

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weather-forecast", () =>
{
    return Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.Run();

namespace Grafana.OpenTelemetry.Sample
{
    public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(this.TemperatureC / 0.5556);
    }
}
