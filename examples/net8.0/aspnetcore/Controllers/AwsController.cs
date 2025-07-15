//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using Amazon.S3;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AwsController(IAmazonS3 client, ILogger<AwsController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<string[]>> ListBuckets()
    {
        var response = await client.ListBucketsAsync();

        var buckets = response.Buckets?.Select(o => o.BucketName).ToArray() ?? [];

        logger.LogInformation("Found {Count} buckets.", buckets.Length);

        return this.Ok(buckets);
    }
}
