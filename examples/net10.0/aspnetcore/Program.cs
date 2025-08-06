//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using Amazon.S3;
using aspnetcore;
using Grafana.OpenTelemetry;
using Microsoft.Data.SqlClient;
using OpenTelemetry.Logs;
using OpenTelemetry.Trace;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddOpenTelemetry(builder => builder.UseGrafana());

builder.Services.AddOpenTelemetry()
    .WithMetrics(builder => builder.UseGrafana())
    .WithTracing(builder => builder.UseGrafana().AddConsoleExporter());

// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("redis:6379"));
builder.Services.AddScoped(sp => sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

// Microsoft SQL Server
builder.Services.AddTransient(sp =>
{
    var connectionString = "Server=mssql,1433;Database=master;User=sa;Password=Password12345%%;Encrypt=False;TrustServerCertificate=True";
    return new SqlConnection(connectionString);
});

// AWS SDKs
builder.Services.AddSingleton<IAmazonS3>((_) => new AmazonS3Client(new AmazonS3Config() { ForcePathStyle = true }));

builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddTodoApp();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.MapTodoApp();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
