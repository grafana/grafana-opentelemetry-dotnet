//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System.Linq;
using OpenTelemetry.Resources;
using Xunit;

namespace Grafana.OpenTelemetry.Tests
{
    public class ResourceBuilderExtensionTest
    {
        [Fact]
        public void StableServiceInstanceId()
        {
            var resource1 = ResourceBuilder
                .CreateEmpty()
                .AddGrafanaResource(new GrafanaOpenTelemetrySettings())
                .Build()
                .Attributes
                .ToDictionary(x => x.Key, x => x.Value);
            var resource2 = ResourceBuilder
                .CreateEmpty()
                .AddGrafanaResource(new GrafanaOpenTelemetrySettings())
                .Build()
                .Attributes
                .ToDictionary(x => x.Key, x => x.Value);

            Assert.NotNull(resource1["service.instance.id"]);
            Assert.NotNull(resource2["service.instance.id"]);
            Assert.Equal(resource1["service.instance.id"], resource2["service.instance.id"]);
        }

        [Fact]
        public void OverrideServiceInstanceId()
        {
            var settings = new GrafanaOpenTelemetrySettings
            {
                ServiceInstanceId = "test-id"
            };
            var resource1 = ResourceBuilder
                .CreateEmpty()
                .AddGrafanaResource(settings)
                .Build()
                .Attributes
                .ToDictionary(x => x.Key, x => x.Value);

            Assert.NotNull(resource1["service.instance.id"]);
            Assert.Equal(resource1["service.instance.id"], "test-id");
        }
    }
}
