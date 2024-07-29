//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
    internal class AzureResourceInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.AzureResource;

        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            return builder.AddAzureAppServiceDetector();
        }
    }
}
