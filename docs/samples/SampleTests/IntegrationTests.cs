//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System.Diagnostics;
using System.Net.Http.Json;
using Grafana.OpenTelemetry.Sample;
using MartinCostello.Logging.XUnit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;

namespace SampleTests;

public class IntegrationTests(ITestOutputHelper outputHelper)
{
    [Fact]
    public async Task Application_Generates_Telemetry()
    {
        // Arrange
        await using var fixture = new OpenTelemetryApplication(outputHelper);
        using var client = fixture.CreateDefaultClient();

        var request = new HttpRequestMessage(HttpMethod.Get, "/");
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("User-Agent", "SampleTests");

        // Act
        var actual = await client.GetFromJsonAsync<WeatherForecast[]>("/weather-forecast", TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(actual);
        Assert.NotEmpty(actual);
        Assert.All(actual, item =>
        {
            Assert.NotNull(item);
            Assert.InRange(item.TemperatureC, -20, 55);
            Assert.Contains(
                item.Summary,
                new string[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" });
        });

        Assert.NotEmpty(fixture.Logs);
        //Assert.NotEmpty(fixture.Metrics); // TODO Why is this empty?
        //Assert.NotEmpty(fixture.Traces);
    }

    public class OpenTelemetryApplication(ITestOutputHelper outputHelper) : WebApplicationFactory<WeatherForecast>, ITestOutputHelperAccessor
    {
        public ITestOutputHelper? OutputHelper { get; set; } = outputHelper;

        public IList<LogRecord> Logs { get; } = [];

        public IList<Metric> Metrics { get; } = [];

        public IList<Activity> Traces { get; } = [];

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((configBuilder) =>
            {
                var config = new[]
                {
                    KeyValuePair.Create<string, string?>("OTEL_METRIC_EXPORT_INTERVAL", "00:00:01"),
                };

                configBuilder.AddInMemoryCollection(config);
            });

            builder.ConfigureLogging(logging => logging.ClearProviders().AddXUnit(this));

            builder.ConfigureTestServices(services =>
            {
                services.AddOpenTelemetry()
                        .WithLogging((builder) => builder.AddInMemoryExporter(Logs))
                        .WithMetrics((builder) => builder.AddInMemoryExporter(Metrics))
                        /*.WithTracing((builder) => builder.AddInMemoryExporter(Traces))*/;
            });
        }
    }
}
