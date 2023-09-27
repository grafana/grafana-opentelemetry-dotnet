using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
    internal static class ResourceBuilderExtension
    {
        public static ResourceBuilder AddGrafanaResource(this ResourceBuilder resourceBuilder, GrafanaOpenTelemetrySettings settings)
        {
            return resourceBuilder
                .AddDetector(new GrafanaOpenTelemetryResourceDetector(settings))
                .AddService(
                    serviceName: settings.ServiceName,
                    serviceVersion: settings.ServiceVersion,
                    serviceInstanceId: settings.ServiceInstanceId);

        }
    }
}
