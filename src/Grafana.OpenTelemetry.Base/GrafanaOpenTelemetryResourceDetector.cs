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
            var attributes = new List<KeyValuePair<string, object>>(new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>(ResourceKey_DistroName, ResourceValue_DistroName),
                new KeyValuePair<string, object>(ResourceKey_DistroVersion, GetDistroVersion()),
                new KeyValuePair<string, object>(ResourceKey_DeploymentEnvironment, _settings.DeploymentEnvironment)
            });

            attributes.AddRange(_settings.ResourceAttributes);

            return new Resource(attributes);
        }

        static internal string GetDistroVersion()
        {
            var informationalVersion = typeof(GrafanaOpenTelemetryResourceDetector)
                .Assembly
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                .InformationalVersion;

            if (string.IsNullOrWhiteSpace(informationalVersion))
            {
                    informationalVersion = "0.0.0";
            }

            // A Git hash is appended to the informational version after a "+" character. That's of limited use and
            // therefore removed here.
            return informationalVersion.Split('+')[0];
        }
    }
}
