//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;

namespace Grafana.OpenTelemetry
{
    /// <summary>
    /// Helper class for Grafana Cloud configuration.
    /// </summary>
    internal sealed class GrafanaCloudConfigurationHelper
    {
        private const string PathExtensionTraces = "/v1/traces";
        private const string PathExtensionMetrics = "/v1/metrics";
        private const string PathExtensionLogs = "/v1/logs";

        private readonly string _zone;
        private readonly string _instanceId;
        private readonly string _apiKey;

        public GrafanaCloudConfigurationHelper(string zone, string instanceId, string apiKey)
        {
            _zone = zone;
            _instanceId = instanceId;
            _apiKey = apiKey;
        }

        public Uri OtlpEndpointTraces
        {
            get => new Uri($"{GetOtlpEndpointBase()}{PathExtensionTraces}");
        }

        public Uri OtlpEndpointMetrics
        {
            get => new Uri($"{GetOtlpEndpointBase()}{PathExtensionMetrics}");
        }

        public Uri OtlpEndpointLogs
        {
            get => new Uri($"{GetOtlpEndpointBase()}{PathExtensionLogs}");
        }

        public string OtlpAuthorizationHeader
        {
            get
            {
                var authorizationValue = Convert.ToBase64String(
                    System.Text.Encoding.UTF8.GetBytes($"{_instanceId}:{_apiKey}")
                );

                return $"Authorization=Basic {authorizationValue}";
            }
        }

        private string GetOtlpEndpointBase() => $"https://otlp-gateway-{_zone}.grafana.net/otlp";
    }
}
