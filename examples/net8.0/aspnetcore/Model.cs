namespace aspnetcore;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public string DbPath { get; }

    public BloggingContext(DbContextOptions<BloggingContext> dbContextOptions): base(dbContextOptions)
    {
    }

    //// The following configures EF to create a Sqlite database file in the
    //// special "local" folder for your platform.
    //protected override void OnConfiguring(DbContextOptionsBuilder options)
    //    => options.UseSqlite($"Data Source={DbPath}");
}

public static class BloggingContextInitializer
{
    public static void Initialize(BloggingContext context)
    {
        if (context.Posts.Any())
        {
            return;
        }

        var blog = new Blog { Url = "https://devblogs.microsoft.com/dotnet/", };
        context.Blogs.Add(blog);
        context.SaveChanges();

        var posts = new List<Post>
        {
            new Post { Title = "Intro to C#", Content = "C# is a language.", Blog = blog },
            new Post { Title = "Intro to F#", Content = "F# is a language.", Blog = blog },
            new Post { Title = "Intro to VB.NET", Content = "VB.NET is a language.", Blog = blog },
        };
        context.Posts.AddRange(posts);
        context.SaveChanges();
    }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; } = new();
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}
