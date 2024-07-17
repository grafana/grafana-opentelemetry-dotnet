//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

#if !NETSTANDARD

using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
    internal class HostResourceInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.HostResource;

        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            return builder.AddHostDetector();
        }
    }
}

#endif
