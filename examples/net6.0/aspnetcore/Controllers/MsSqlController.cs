using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace aspnetcore.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class MsSqlController : ControllerBase
{
    private readonly ILogger<MsSqlController> _logger;
    private readonly SqlConnection _db;

    public MsSqlController(ILogger<MsSqlController> logger, SqlConnection db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> Tables()
    {
        this._db.Open();

        using (var command = _db.CreateCommand())
        {
            command.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
            var tables = new List<string>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    tables.Add(reader.GetString(0));
                }
            }
            return Ok(tables);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> ServerInfo()
    {
        this._db.Open();

        using (var command = _db.CreateCommand())
        {
            command.CommandText = "sp_server_info";
            command.CommandType = CommandType.StoredProcedure;

            var serverInfo = new List<string>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    serverInfo.Add($"ID={reader.GetInt32(0)} , NAME={reader.GetString(1)} , VALUE={reader.GetString(2)}");
                }
            }

            return Ok(serverInfo);
        }
    }
}
