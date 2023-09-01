using System;

namespace Grafana.OpenTelemetry;

/// <summary>
/// Helper class for Grafana Cloud configuration.
/// </summary>
internal class GrafanaCloudConfigurationHelper
{
    private string _zone;
    public string _instanceId;
    public string _apiKey;

    public GrafanaCloudConfigurationHelper(string zone, string instanceId, string apiKey)
    {
        _zone = zone;
        _instanceId = instanceId;
        _apiKey = apiKey;
    }

    public Uri OtlpEndpoint
    {
        get => new Uri($"https://otlp-gateway-{_zone}.grafana.net/otlp");
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
}
