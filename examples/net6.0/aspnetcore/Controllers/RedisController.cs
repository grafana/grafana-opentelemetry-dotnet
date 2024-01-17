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
public class RedisController : ControllerBase
{
    const string LIST_KEY = "list";
    private readonly ILogger<RedisController> _logger;
    private IDatabase _redisDb;

    public RedisController(ILogger<RedisController> logger, IDatabase database)
    {
        _logger = logger;
        _redisDb = database;
    }

    [HttpGet]
    // public async Task<ActionResult<string>> LeftPush([FromBody] LeftPushBody data)
    public async Task<ActionResult<string>> LeftPush()
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(500);
        }

        var data = new LeftPushBody { Name = "test" };
        var length = await _redisDb.ListLeftPushAsync(LIST_KEY, data.Name);
        _logger.LogInformation($"LeftPush: {data.Name} - {length}");
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<string>> LeftPop()
    {
        var value = await _redisDb.ListLeftPopAsync(LIST_KEY);
        _logger.LogInformation($"LeftPop: {value}");
        return Ok(value);
    }

    public class LeftPushBody
    {
        [Required]
        public string? Name { get; set; }
    }
}
