//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

#if !NETSTANDARD

using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
    internal class ProcessResourceInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.ProcessResource;

        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            return builder.AddProcessDetector();
        }
    }
}

#endif
