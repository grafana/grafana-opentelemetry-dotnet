//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using Grafana.OpenTelemetry;
using OpenTelemetry.Trace;
using Microsoft.Data.SqlClient;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenTelemetry()
    .WithMetrics(builder => builder.UseGrafana())
    .WithTracing(builder => builder.UseGrafana().AddConsoleExporter());

// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(
    sp => ConnectionMultiplexer.Connect("redis:6379"));
builder.Services.AddScoped(
    sp => sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

// MSSQL
builder.Services.AddTransient(sp =>
{
    var connectionString = "Server=mssql,1433;Database=master;User=sa;Password=Password12345%%;Encrypt=False;TrustServerCertificate=True";
    return new SqlConnection(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));
app.Run();
