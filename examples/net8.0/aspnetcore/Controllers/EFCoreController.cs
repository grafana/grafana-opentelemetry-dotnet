using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class EFCoreController : Controller
{
    readonly BloggingContext _context;

    public EFCoreController(BloggingContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Blog>> ListBlogs()
    {
        var blogs = _context.Blogs.ToList();
        return Ok(blogs);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> ListPosts([FromQuery] int blogId)
    {
        var blog = await _context.Blogs.FirstAsync(x => x.BlogId == blogId);
        var posts = blog.Posts.Select(x => x.Title).ToList();
        return Ok(posts);
    }
}
