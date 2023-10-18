//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using Grafana.OpenTelemetry;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using Xunit;

namespace Grafana.OpenTelemetry.Tests
{
    public class OpenTelemetryLoggerOptionsExtensionsTest
    {
        [Fact]
        public void BuildDefault()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddOpenTelemetry(logging =>
                {
                    logging.UseGrafana();
                });
            });

            var logger = loggerFactory.CreateLogger<OpenTelemetryLoggerOptionsExtensionsTest>();
        }
    }
}
