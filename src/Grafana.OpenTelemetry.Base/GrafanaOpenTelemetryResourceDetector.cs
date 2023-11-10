//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
    internal class GrafanaOpenTelemetryResourceDetector : IResourceDetector
    {
        internal const string ResourceKey_DistroName = "telemetry.distro.name";
        internal const string ResourceKey_DistroVersion = "telemetry.distro.version";
        internal const string ResourceKey_DeploymentEnvironment = "deployment.environment";
        internal const string ResourceValue_DistroName = "grafana-opentelemetry-dotnet";

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
                new KeyValuePair<string, object>(ResourceKey_DistroName, ResourceValue_DistroName),
                new KeyValuePair<string, object>(ResourceKey_DistroVersion, FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion),
                new KeyValuePair<string, object>(ResourceKey_DeploymentEnvironment, _settings.DeploymentEnvironment)
            });
        }
    }
}
