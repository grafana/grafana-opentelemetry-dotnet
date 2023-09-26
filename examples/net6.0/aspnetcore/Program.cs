using Grafana.OpenTelemetry;
using Microsoft.Data.SqlClient;
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
    var connectionString = "Server=mssql,1433;Database=master;User=sa;Password=Password12345%%;";
    return new SqlConnection(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
