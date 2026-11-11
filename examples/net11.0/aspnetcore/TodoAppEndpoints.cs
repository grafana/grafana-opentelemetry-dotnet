//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using Microsoft.EntityFrameworkCore;

namespace aspnetcore;

public static class TodoAppEndpoints
{
    public static IServiceCollection AddTodoApp(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<TodoRepository>();

        services.AddDbContext<TodoContext>((serviceProvider, options) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var dataDirectory = configuration["DataDirectory"];

            if (string.IsNullOrEmpty(dataDirectory) || !Path.IsPathRooted(dataDirectory))
            {
                var environment = serviceProvider.GetRequiredService<IHostEnvironment>();
                dataDirectory = Path.Join(environment.ContentRootPath, "App_Data");
            }

            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            var databaseFile = Path.Join(dataDirectory, "TodoApp.db");

            options.UseSqlite("Data Source=" + databaseFile);
        });

        return services;
    }

    public static IEndpointRouteBuilder MapTodoApp(this IEndpointRouteBuilder builder)
    {
        var todos = builder.MapGroup("/api/todo/items").WithTags("Todo");
        {
            todos.MapPost("/", async (CreateTodoItemModel model, TodoRepository repository) =>
            {
                var id = await repository.AddItemAsync(model.Text);
                return Results.Created($"/api/items/{id.Id}", new { Id = id });
            });

            todos.MapGet("/", async (TodoRepository repository) => await repository.GetItemsAsync());

            todos.MapGet("/{id}", async (Guid id, TodoRepository repository) => await repository.GetItemAsync(id));

            todos.MapPost("/{id}/complete", async (Guid id, TodoRepository repository) =>
            {
                return await repository.CompleteItemAsync(id) switch
                {
                    true => Results.NoContent(),
                    false => Results.Problem(statusCode: StatusCodes.Status400BadRequest),
                    _ => Results.Problem(statusCode: StatusCodes.Status404NotFound),
                };
            });

            todos.MapDelete("/{id}", async (Guid id, TodoRepository repository) =>
            {
                var deleted = await repository.DeleteItemAsync(id);
                return deleted switch
                {
                    true => Results.NoContent(),
                    false => Results.Problem(statusCode: StatusCodes.Status404NotFound),
                };
            });
        }

        return builder;
    }

    public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
    {
        public DbSet<TodoItem> Items { get; set; } = default!;
    }

    public class TodoRepository(TimeProvider timeProvider, TodoContext context)
    {
        public async Task<TodoItem> AddItemAsync(string text)
        {
            await this.EnsureDatabaseAsync();

            var item = new TodoItem
            {
                CreatedAt = this.UtcNow(),
                Text = text
            };

            context.Add(item);

            await context.SaveChangesAsync();

            return item;
        }

        public async Task<bool?> CompleteItemAsync(Guid itemId)
        {
            var item = await this.GetItemAsync(itemId);

            if (item is null)
            {
                return null;
            }

            if (item.CompletedAt.HasValue)
            {
                return false;
            }

            item.CompletedAt = this.UtcNow();

            context.Items.Update(item);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteItemAsync(Guid itemId)
        {
            var item = await this.GetItemAsync(itemId);

            if (item is null)
            {
                return false;
            }

            context.Items.Remove(item);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<TodoItem?> GetItemAsync(Guid itemId)
        {
            await this.EnsureDatabaseAsync();

            return await context.Items.FindAsync([itemId]);
        }

        public async Task<IList<TodoItem>> GetItemsAsync()
        {
            await this.EnsureDatabaseAsync();

            return await context.Items
                .OrderBy(x => x.CompletedAt.HasValue)
                .ThenBy(x => x.CreatedAt)
                .ToListAsync();
        }

        private async Task EnsureDatabaseAsync() => await context.Database.EnsureCreatedAsync();

        private DateTime UtcNow() => timeProvider.GetUtcNow().UtcDateTime;
    }

    public class CreateTodoItemModel
    {
        public string Text { get; set; } = string.Empty;
    }

    public class TodoItem
    {
        public Guid Id { get; set; }

        public string Text { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}
