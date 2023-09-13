using System;
using System.Collections.Generic;
using Grafana.OpenTelemetry;
using Xunit;

namespace Grafana.OpenTelemetry.Tests
{
    public class GrafanaOpenTelemetrySettingsTest
    {
        public GrafanaOpenTelemetrySettingsTest()
        {
            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.DisableInstrumentationsEnvVarName, null);
        }

        [Fact]
        public void DefaultInstrumentations()
        {
            var settings = new GrafanaOpenTelemetrySettings();

            Assert.Equal(new HashSet<Instrumentation>((Instrumentation[])Enum.GetValues(typeof(Instrumentation))), settings.Instrumentations);
        }

        [Fact]
        public void DisableInstrumentations()
        {
            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.DisableInstrumentationsEnvVarName, "Process,NetRuntime");

            var settings = new GrafanaOpenTelemetrySettings();

            var expected = new HashSet<Instrumentation>((Instrumentation[])Enum.GetValues(typeof(Instrumentation)));
            expected.ExceptWith(new Instrumentation[] { Instrumentation.Process, Instrumentation.NetRuntime });

            Assert.Equal(expected, settings.Instrumentations);
        }
    }
}
