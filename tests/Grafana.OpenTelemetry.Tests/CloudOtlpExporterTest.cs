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
    }
}
