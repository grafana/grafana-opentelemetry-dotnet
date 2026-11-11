//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class HttpClientController(HttpClient client) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> Get()
    {
        var content = await client.GetStringAsync("https://postman-echo.com/get?hello=world");
        return Ok(content);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> GetError()
    {
        var response = await client.GetAsync("https://postman-echo.com/status/500");
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpPost]
    public async Task<ActionResult<string>> Post()
    {
        using var body = new StringContent("Hello World");
        using var response = await client.PostAsync("https://postman-echo.com/post", body);

        var content = await response.Content.ReadAsStringAsync();

        return Ok(content);
    }
}
