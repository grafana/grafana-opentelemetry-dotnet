//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using Grafana.OpenTelemetry;
using Xunit;

namespace Grafana.OpenTelemetry.Tests
{
    public class GrafanaCloudConfigurationHelperTest
    {
        [Fact]
        public void OtlpEndpoint()
        {
            var helper = new GrafanaCloudConfigurationHelper("prod-us-east-0", "", "");

            Assert.Equal(
                new Uri($"https://otlp-gateway-prod-us-east-0.grafana.net/otlp/v1/traces"),
                helper.OtlpEndpointTraces);

            Assert.Equal(
                new Uri($"https://otlp-gateway-prod-us-east-0.grafana.net/otlp/v1/metrics"),
                helper.OtlpEndpointMetrics);

            Assert.Equal(
                new Uri($"https://otlp-gateway-prod-us-east-0.grafana.net/otlp/v1/logs"),
                helper.OtlpEndpointLogs);
        }

        [Fact]
        public void OtlpAuthorizationHeader()
        {
            var helper = new GrafanaCloudConfigurationHelper("", "701628", "a_secret");

            Assert.Equal(
                "Authorization=Basic NzAxNjI4OmFfc2VjcmV0",
                helper.OtlpAuthorizationHeader);
        }
    }
}
