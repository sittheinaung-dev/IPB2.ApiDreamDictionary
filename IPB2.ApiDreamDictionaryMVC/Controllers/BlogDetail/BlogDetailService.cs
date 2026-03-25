using IPB2.ApiDreamDictionary.Database.AppDbContextModels;
using Microsoft.EntityFrameworkCore;

namespace IPB2.ApiDreamDictionaryMVC.Controllers.BlogDetail;

public interface IBlogDetailService
{
    Task<List<BlogDetailResponse>> GetBlogDetailsAsync();
    Task<BlogDetailResponse?> GetBlogDetailByIdAsync(int id);
    Task<List<BlogDetailResponse>> GetBlogDetailsByBlogIdAsync(int blogId);
    Task<List<BlogDetailResponse>> SearchBlogDetailsAsync(string keyword);
    Task CreateBlogDetailAsync(BlogDetailRequest request);
}

public class BlogDetailService : IBlogDetailService
{
    private readonly AppDbContext _db;

    public BlogDetailService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<BlogDetailResponse>> GetBlogDetailsAsync()
    {
        return await _db.Blogdetails
            .Join(_db.BlogHeaders, 
                  d => d.BlogId, 
                  h => h.BlogId, 
                  (d, h) => new BlogDetailResponse
                  {
                      BlogDetailId = d.BlogDetailId,
                      BlogId = d.BlogId,
                      BlogContent = d.BlogContent,
                      BlogTitle = h.BlogTitle
                  })
            .ToListAsync();
    }

    public async Task<BlogDetailResponse?> GetBlogDetailByIdAsync(int id)
    {
        var detail = await _db.Blogdetails.FindAsync(id);
        if (detail == null) return null;

        var header = await _db.BlogHeaders.FindAsync(detail.BlogId);

        return new BlogDetailResponse
        {
            BlogDetailId = detail.BlogDetailId,
            BlogId = detail.BlogId,
            BlogContent = detail.BlogContent,
            BlogTitle = header?.BlogTitle
        };
    }

    public async Task<List<BlogDetailResponse>> GetBlogDetailsByBlogIdAsync(int blogId)
    {
        return await _db.Blogdetails
            .Where(x => x.BlogId == blogId)
            .Select(x => new BlogDetailResponse
            {
                BlogDetailId = x.BlogDetailId,
                BlogId = x.BlogId,
                BlogContent = x.BlogContent
            })
            .ToListAsync();
    }

    public async Task<List<BlogDetailResponse>> SearchBlogDetailsAsync(string keyword)
    {
        return await _db.Blogdetails
            .Join(_db.BlogHeaders, 
                  d => d.BlogId, 
                  h => h.BlogId, 
                  (d, h) => new BlogDetailResponse
                  {
                      BlogDetailId = d.BlogDetailId,
                      BlogId = d.BlogId,
                      BlogContent = d.BlogContent,
                      BlogTitle = h.BlogTitle
                  })
            .Where(x => x.BlogContent.Contains(keyword) || x.BlogTitle.Contains(keyword))
            .ToListAsync();
    }

    public async Task CreateBlogDetailAsync(BlogDetailRequest request)
    {
        var detail = new Blogdetail
        {
            BlogDetailId = request.BlogDetailId,
            BlogId = request.BlogId,
            BlogContent = request.BlogContent
        };
        _db.Blogdetails.Add(detail);
        await _db.SaveChangesAsync();
    }
}
