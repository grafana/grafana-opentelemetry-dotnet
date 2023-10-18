//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class HttpClientController : ControllerBase
{
    private readonly ILogger<HttpClientController> _logger;

    public HttpClientController(ILogger<HttpClientController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> Get()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("https://postman-echo.com/get?hello=world");
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> GetError()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("http://example.com");
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpPost]
    public async Task<ActionResult<string>> Post()
    {
        var client = new HttpClient();
        var response = await client.PostAsync("https://postman-echo.com/post", new StringContent("Hello World"));
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }
}
