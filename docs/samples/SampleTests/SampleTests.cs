using System.Net.Http.Json;
using Grafana.OpenTelemetry.Sample;
using MartinCostello.Logging.XUnit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;

namespace SampleTests;

public class SampleTests(ITestOutputHelper outputHelper)
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
    }

    public class OpenTelemetryApplication(ITestOutputHelper outputHelper) : WebApplicationFactory<WeatherForecast>, ITestOutputHelperAccessor
    {
        public ITestOutputHelper? OutputHelper { get; set; } = outputHelper;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureLogging(logging => logging.ClearProviders().AddXUnit(this));
        }
    }
}
