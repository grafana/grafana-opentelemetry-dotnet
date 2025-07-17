//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using Microsoft.Extensions.Logging;
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

            logger.LogInformation("This is a test log message.");
        }
    }
}
