//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace aspnetcore.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RedisController(IDatabase database, ILogger<RedisController> logger) : ControllerBase
{
    const string LIST_KEY = "list";

    [HttpGet]
    public async Task<ActionResult<string>> LeftPush()
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(500);
        }

        var data = new LeftPushBody { Name = "test" };
        var length = await database.ListLeftPushAsync(LIST_KEY, data.Name);

        logger.LogInformation("LeftPush: {Name} - {Length}", data.Name, length);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<string>> LeftPop()
    {
        var value = await database.ListLeftPopAsync(LIST_KEY);

        logger.LogInformation("LeftPop: {Value}", value);

        return Ok(value);
    }

    public class LeftPushBody
    {
        [Required]
        public string? Name { get; set; }
    }
}
