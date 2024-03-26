//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using aspnetcore;
using Grafana.OpenTelemetry;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenTelemetry()
    .WithMetrics(builder => builder.UseGrafana())
    .WithTracing(builder => builder.UseGrafana());

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

// EF Core w/ Sqlite
builder.Services.AddDbContext<BloggingContext>(options =>
{
    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);
    options.UseSqlite($"Data Source={Path.Join(path, "blogging.db")}");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<BloggingContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    BloggingContextInitializer.Initialize(context);
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));
app.Run();
