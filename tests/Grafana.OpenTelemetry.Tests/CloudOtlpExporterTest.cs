using System;
using Xunit;

namespace Grafana.OpenTelemetry.Tests
{
    public class CloudOtlpExporterTest
    {
        [Fact]
        public void CloudOtlpExporterEnvVars()
        {
            Environment.SetEnvironmentVariable(CloudOtlpExporter.ApiKeyEnvVarName, "a_secret");
            Environment.SetEnvironmentVariable(CloudOtlpExporter.InstanceIdEnvVarName, "12345");
            Environment.SetEnvironmentVariable(CloudOtlpExporter.ZoneEnvVarName, "test-example-0");
            var settings = new CloudOtlpExporter();

            Assert.Equal("a_secret", settings.ApiKey);
            Assert.Equal("12345", settings.InstanceId);
            Assert.Equal("test-example-0", settings.Zone);
        }

        [Theory]
        [InlineData(null, "12345", "test-example-0")]
        [InlineData("a_secret", null, "test-example-0")]
        [InlineData("a_secret", "12345", null)]
        public void CloudOtlpExporterAnyEnvVarUnset(string apiKey, string instanceId, string zone)
        {
            Environment.SetEnvironmentVariable(CloudOtlpExporter.ApiKeyEnvVarName, apiKey);
            Environment.SetEnvironmentVariable(CloudOtlpExporter.InstanceIdEnvVarName, instanceId);
            Environment.SetEnvironmentVariable(CloudOtlpExporter.ZoneEnvVarName, zone);
            Assert.Throws<ArgumentNullException>(() => new CloudOtlpExporter());
        }
    }
}
