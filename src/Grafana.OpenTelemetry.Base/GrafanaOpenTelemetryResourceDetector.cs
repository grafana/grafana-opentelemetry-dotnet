//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using System.Collections.Generic;
using System.Reflection;
using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
    internal class GrafanaOpenTelemetryResourceDetector : IResourceDetector
    {
        internal const string ResourceKey_DistroName = "telemetry.distro.name";
        internal const string ResourceKey_DistroVersion = "telemetry.distro.version";
        internal const string ResourceKey_DeploymentEnvironment = "deployment.environment";

        private GrafanaOpenTelemetrySettings _settings;

        public GrafanaOpenTelemetryResourceDetector(GrafanaOpenTelemetrySettings settings)
        {
            _settings = settings;
        }

        public Resource Detect()
        {
            var assembly = typeof(GrafanaOpenTelemetryResourceDetector).Assembly;

            return new Resource(new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>(ResourceKey_DistroName, assembly.GetName().Name),
                new KeyValuePair<string, object>(ResourceKey_DistroVersion, assembly.GetName().Version.ToString()),
                new KeyValuePair<string, object>(ResourceKey_DeploymentEnvironment, _settings.DeploymentEnvironment)
            });
        }
    }
}
