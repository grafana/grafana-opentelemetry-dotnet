//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
    internal static class ResourceBuilderExtension
    {
        public static ResourceBuilder AddGrafanaResource(this ResourceBuilder resourceBuilder, GrafanaOpenTelemetrySettings settings)
        {
            var serviceInstanceIdProvided = !string.IsNullOrEmpty(settings.ServiceInstanceId);

            return resourceBuilder
                .AddDetector(new GrafanaOpenTelemetryResourceDetector(settings))
                .AddService(
                    serviceName: settings.ServiceName,
                    serviceVersion: settings.ServiceVersion,
                    serviceInstanceId: serviceInstanceIdProvided ? settings.ServiceInstanceId : null,
                    autoGenerateServiceInstanceId: !serviceInstanceIdProvided);

        }
    }
}
