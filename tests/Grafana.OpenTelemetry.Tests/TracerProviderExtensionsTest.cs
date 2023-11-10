//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Grafana.OpenTelemetry;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Xunit;

namespace Grafana.OpenTelemetry.Tests
{
    public class TracerProviderExtensionsTest
    {
        private static ActivitySource activitySource = new ActivitySource(typeof(TracerProviderExtensionsTest).Name);

        [Fact]
        public void EnableDefaultInstrumentations()
        {
            Sdk
                .CreateTracerProviderBuilder()
                .UseGrafana()
                .Build();
        }

        [Fact]
        public void ResourceAttributes()
        {
            var spans = new List<(Activity, Resource)>();

            Sdk
                .CreateTracerProviderBuilder()
                .UseGrafana()
                .AddProcessor(new SimpleActivityExportProcessor(new InMemoryResourceExporter<Activity>(spans)))
                .AddSource(activitySource.Name)
                .Build();

            var span = activitySource.StartActivity("root");
            span.Stop();
            span.Dispose();

            Assert.Single(spans);

            var resourceTags = new Dictionary<string, string>();

            foreach (var tag in spans[0].Item2.Attributes)
            {
                resourceTags.Add(tag.Key, (string)tag.Value);
            }

            Assert.Equal("grafana-opentelemetry-dotnet", resourceTags["telemetry.distro.name"]);
            Assert.NotNull(resourceTags["telemetry.distro.version"]);

            Assert.NotNull(resourceTags["service.name"]);
            Assert.NotNull(resourceTags["service.instance.id"]);
            Assert.NotNull(resourceTags["service.version"]);
            Assert.NotNull(resourceTags["deployment.environment"]);
        }

        [Fact]
        public void OverrideResourceAttributes()
        {
            var spans = new List<(Activity, Resource)>();

            Sdk
                .CreateTracerProviderBuilder()
                .UseGrafana()
                .ConfigureResource(resourceBuilder =>
                {
                    resourceBuilder
                        .AddService(
                        serviceName: "a",
                        serviceVersion: "0",
                        serviceInstanceId: "b");
                })
                .AddProcessor(new SimpleActivityExportProcessor(new InMemoryResourceExporter<Activity>(spans)))
                .AddSource(activitySource.Name)
                .Build();

            var span = activitySource.StartActivity("root");
            span.Stop();
            span.Dispose();

            Assert.Single(spans);

            var resourceTags = new Dictionary<string, string>();

            foreach (var tag in spans[0].Item2.Attributes)
            {
                resourceTags.Add(tag.Key, (string)tag.Value);
            }

            Assert.Equal("a", resourceTags["service.name"]);
            Assert.Equal("b", resourceTags["service.instance.id"]);
            Assert.Equal("0", resourceTags["service.version"]);
        }

        [Fact]
        public void OverrideResourceAttributesEnvironment()
        {
            Environment.SetEnvironmentVariable(GrafanaOpenTelemetrySettings.ServiceNameEnvVarName, "service-name");

            var spans = new List<(Activity, Resource)>();

            Sdk
                .CreateTracerProviderBuilder()
                .UseGrafana()
                .AddProcessor(new SimpleActivityExportProcessor(new InMemoryResourceExporter<Activity>(spans)))
                .AddSource(activitySource.Name)
                .Build();

            var span = activitySource.StartActivity("root");
            span.Stop();
            span.Dispose();

            Assert.Single(spans);

            var resourceTags = new Dictionary<string, string>();

            foreach (var tag in spans[0].Item2.Attributes)
            {
                resourceTags.Add(tag.Key, (string)tag.Value);
            }

            Assert.Equal("service-name", resourceTags["service.name"]);
        }
    }
}
