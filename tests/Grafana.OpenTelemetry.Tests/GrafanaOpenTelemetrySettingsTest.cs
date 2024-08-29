//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using System.Collections.Generic;
using System.Reflection;
using Grafana.OpenTelemetry;
using Xunit;

namespace Grafana.OpenTelemetry.Tests
{
    public class GrafanaOpenTelemetrySettingsTest
    {
        public GrafanaOpenTelemetrySettingsTest()
        {
            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.DisableInstrumentationsEnvVarName, null);
            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.ServiceNameEnvVarName, null);
        }

        [Fact(Skip = "provider builders crashes on enabling AWSLambda instrumentation by default")]
        public void DefaultInstrumentations()
        {
            var settings = new GrafanaOpenTelemetrySettings();

            Assert.Equal(new HashSet<Instrumentation>((Instrumentation[])Enum.GetValues(typeof(Instrumentation))), settings.Instrumentations);
        }

        [Fact]
        public void DefaultInstrumentationsWithout()
        {
            var settings = new GrafanaOpenTelemetrySettings();

            // De-activate AWSLambda instrumenation until this issue is resolved: https://github.com/grafana/app-o11y/issues/378
            //
            // Once it's resolved, this test can be removed and the `DefaultInstrumentation` test can be re-activated.
            var expectedSettings = new HashSet<Instrumentation>((Instrumentation[])Enum.GetValues(typeof(Instrumentation)));
            expectedSettings.Remove(Instrumentation.AWSLambda);

            Assert.Equal(expectedSettings, settings.Instrumentations);
        }

        [Fact]
        public void DisableInstrumentations()
        {
            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.DisableInstrumentationsEnvVarName, "Process,NetRuntime");

            var settings = new GrafanaOpenTelemetrySettings();

            Assert.DoesNotContain(Instrumentation.Process, settings.Instrumentations);
            Assert.DoesNotContain(Instrumentation.NetRuntime, settings.Instrumentations);

            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.DisableInstrumentationsEnvVarName, null);
        }

        [Fact]
        public void DisableInstrumentationsColon()
        {
            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.DisableInstrumentationsEnvVarName, "Process:NetRuntime");

            var settings = new GrafanaOpenTelemetrySettings();

            Assert.DoesNotContain(Instrumentation.Process, settings.Instrumentations);
            Assert.DoesNotContain(Instrumentation.NetRuntime, settings.Instrumentations);

            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.DisableInstrumentationsEnvVarName, null);
        }

        [Fact]
        public void DisableResourceDetectors()
        {
            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.DisableResourceDetectorsEnvVarName, "Host,Process");

            var settings = new GrafanaOpenTelemetrySettings();

            Assert.DoesNotContain(ResourceDetector.Host, settings.ResourceDetectors);
            Assert.DoesNotContain(ResourceDetector.Process, settings.ResourceDetectors);

            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.DisableResourceDetectorsEnvVarName, null);
        }

        [Fact]
        public void DisableResourceDetectorsColon()
        {
            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.DisableResourceDetectorsEnvVarName, "Host:Process");

            var settings = new GrafanaOpenTelemetrySettings();

            Assert.DoesNotContain(ResourceDetector.Host, settings.ResourceDetectors);
            Assert.DoesNotContain(ResourceDetector.Process, settings.ResourceDetectors);

            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.DisableResourceDetectorsEnvVarName, null);
        }

        [Fact]
        public void SetSingleResourceDetector()
        {
            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.ResourceDetectorsEnvVarName, "Container");

            var settings = new GrafanaOpenTelemetrySettings();

            Assert.Single(settings.ResourceDetectors);
            Assert.Contains(ResourceDetector.Container, settings.ResourceDetectors);

            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.ResourceDetectorsEnvVarName, null);
        }

        [Fact]
        public void SetResourceDetectors()
        {
            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.ResourceDetectorsEnvVarName, "Container,Process");

            var settings = new GrafanaOpenTelemetrySettings();

            Assert.Equal(2, settings.ResourceDetectors.Count);
            Assert.Contains(ResourceDetector.Container, settings.ResourceDetectors);
            Assert.Contains(ResourceDetector.Process, settings.ResourceDetectors);

            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.ResourceDetectorsEnvVarName, null);
        }

        [Fact]
        public void SetResourceDetectorsColon()
        {
            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.ResourceDetectorsEnvVarName, "Container:Process");

            var settings = new GrafanaOpenTelemetrySettings();

            Assert.Equal(2, settings.ResourceDetectors.Count);
            Assert.Contains(ResourceDetector.Container, settings.ResourceDetectors);
            Assert.Contains(ResourceDetector.Process, settings.ResourceDetectors);

            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.ResourceDetectorsEnvVarName, null);
        }

        [Fact]
        public void SetAndDisableResourceDetectors()
        {
            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.ResourceDetectorsEnvVarName, "Host,Container");
            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.DisableResourceDetectorsEnvVarName, "Container");

            var settings = new GrafanaOpenTelemetrySettings();

            Assert.Single(settings.ResourceDetectors);
            Assert.Contains(ResourceDetector.Host, settings.ResourceDetectors);

            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.ResourceDetectorsEnvVarName, null);
            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.DisableResourceDetectorsEnvVarName, null);
        }

        [Fact]
        public void DefaultServiceName()
        {
            var settings = new GrafanaOpenTelemetrySettings();

            Assert.Equal(Assembly.GetEntryAssembly()?.GetName().Name ?? System.Diagnostics.Process.GetCurrentProcess().ProcessName, settings.ServiceName);
        }
    }
}
