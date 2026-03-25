using Microsoft.AspNetCore.Mvc;
using IPB2.ApiDreamDictionaryMVC.Controllers.BlogDetail;

namespace IPB2.ApiDreamDictionaryMVC.Controllers.BlogHeader;

public class BlogHeaderController : Controller
{
    private readonly IBlogHeaderService _blogHeaderService;
    private readonly IBlogDetailService _blogDetailService;

    public BlogHeaderController(IBlogHeaderService blogHeaderService, IBlogDetailService blogDetailService)
    {
        _blogHeaderService = blogHeaderService;
        _blogDetailService = blogDetailService;
    }

    public async Task<IActionResult> Index()
    {
        var blogs = await _blogHeaderService.GetBlogHeadersAsync();
        return View(blogs);
    }

    public async Task<IActionResult> Details(int id)
    {
        var header = await _blogHeaderService.GetBlogHeaderByIdAsync(id);
        if (header == null) return NotFound();

        var details = await _blogDetailService.GetBlogDetailsByBlogIdAsync(id);
        
        ViewBag.HeaderTitle = header.BlogTitle;
        ViewBag.HeaderId = header.BlogId;
        
        return View(details);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BlogHeaderRequest request)
    {
        if (ModelState.IsValid)
        {
            await _blogHeaderService.CreateBlogHeaderAsync(request);
            return RedirectToAction(nameof(Index));
        }
        return View(request);
    }
}
