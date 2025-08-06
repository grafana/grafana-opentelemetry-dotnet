//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace aspnetcore.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class MsSqlController(SqlConnection db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> Tables()
    {
        await db.OpenAsync();

        await using var command = db.CreateCommand();

        command.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";

        var tables = new List<string>();

        await using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                tables.Add(reader.GetString(0));
            }
        }

        return Ok(tables);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> ServerInfo()
    {
        await db.OpenAsync();

        await using var command = db.CreateCommand();

        command.CommandText = "sp_server_info";
        command.CommandType = CommandType.StoredProcedure;

        var serverInfo = new List<string>();

        await using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                serverInfo.Add($"ID={reader.GetInt32(0)} , NAME={reader.GetString(1)} , VALUE={reader.GetString(2)}");
            }
        }

        return Ok(serverInfo);
    }
}
