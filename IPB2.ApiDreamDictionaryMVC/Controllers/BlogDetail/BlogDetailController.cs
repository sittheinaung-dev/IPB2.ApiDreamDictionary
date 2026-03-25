using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IPB2.ApiDreamDictionaryMVC.Controllers.BlogHeader;

namespace IPB2.ApiDreamDictionaryMVC.Controllers.BlogDetail;

public class BlogDetailController : Controller
{
    private readonly IBlogDetailService _blogDetailService;
    private readonly IBlogHeaderService _blogHeaderService;

    public BlogDetailController(IBlogDetailService blogDetailService, IBlogHeaderService blogHeaderService)
    {
        _blogDetailService = blogDetailService;
        _blogHeaderService = blogHeaderService;
    }

    public async Task<IActionResult> Index(string searchKeyword = "")
    {
        ViewBag.SearchKeyword = searchKeyword;
        List<BlogDetailResponse> details;

        if (string.IsNullOrWhiteSpace(searchKeyword))
        {
            details = await _blogDetailService.GetBlogDetailsAsync();
        }
        else
        {
            details = await _blogDetailService.SearchBlogDetailsAsync(searchKeyword);
        }

        return View(details);
    }

    public async Task<IActionResult> Create()
    {
        var headers = await _blogHeaderService.GetBlogHeadersAsync();
        ViewBag.BlogId = new SelectList(headers, "BlogId", "BlogTitle");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BlogDetailRequest request)
    {
        if (ModelState.IsValid)
        {
            await _blogDetailService.CreateBlogDetailAsync(request);
            return RedirectToAction(nameof(Index));
        }
        var headers = await _blogHeaderService.GetBlogHeadersAsync();
        ViewBag.BlogId = new SelectList(headers, "BlogId", "BlogTitle", request.BlogId);
        return View(request);
    }
}
