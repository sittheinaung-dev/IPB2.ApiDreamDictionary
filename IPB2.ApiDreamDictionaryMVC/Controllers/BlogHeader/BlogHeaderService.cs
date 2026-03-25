using IPB2.ApiDreamDictionary.Database.AppDbContextModels;
using Microsoft.EntityFrameworkCore;

namespace IPB2.ApiDreamDictionaryMVC.Controllers.BlogHeader;

public interface IBlogHeaderService
{
    Task<List<BlogHeaderResponse>> GetBlogHeadersAsync();
    Task<BlogHeaderResponse?> GetBlogHeaderByIdAsync(int id);
    Task CreateBlogHeaderAsync(BlogHeaderRequest request);
}

public class BlogHeaderService : IBlogHeaderService
{
    private readonly AppDbContext _db;

    public BlogHeaderService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<BlogHeaderResponse>> GetBlogHeadersAsync()
    {
        return await _db.BlogHeaders
            .Select(x => new BlogHeaderResponse
            {
                BlogId = x.BlogId,
                BlogTitle = x.BlogTitle
            })
            .ToListAsync();
    }

    public async Task<BlogHeaderResponse?> GetBlogHeaderByIdAsync(int id)
    {
        var blog = await _db.BlogHeaders.FindAsync(id);
        if (blog == null) return null;

        return new BlogHeaderResponse
        {
            BlogId = blog.BlogId,
            BlogTitle = blog.BlogTitle
        };
    }

    public async Task CreateBlogHeaderAsync(BlogHeaderRequest request)
    {
        var blog = new IPB2.ApiDreamDictionary.Database.AppDbContextModels.BlogHeader
        {
            BlogId = request.BlogId,
            BlogTitle = request.BlogTitle
        };
        _db.BlogHeaders.Add(blog);
        await _db.SaveChangesAsync();
    }
}
