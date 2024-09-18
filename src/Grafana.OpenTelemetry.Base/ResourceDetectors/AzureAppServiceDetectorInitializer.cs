//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
    internal class AzureAppServiceDetectorInitializer : ResourceDetectorInitializer
    {
        public override ResourceDetector Id { get; } = ResourceDetector.AzureAppService;

        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Resources.Azure",
                "OpenTelemetry.Resources.AzureResourceBuilderExtensions",
                "AddAzureAppServiceDetector",
                new object[] { builder });
            return builder;
        }
    }
}
