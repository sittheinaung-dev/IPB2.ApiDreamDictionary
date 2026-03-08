using IPB2.ApiDreamDictionary.Database.AppDbContextModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPB2.ApiDreamDictionary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DreamDictionaryController : ControllerBase
    {
        private readonly AppDbContext _db;

        public DreamDictionaryController()
        {
            _db=new AppDbContext();
        }

        [HttpGet]
        public IActionResult GetBlogHeader()
        {
            var BlogHeader = _db.BlogHeaders.ToList();
            return Ok(BlogHeader);
            
        }

        [HttpGet("BlogDetail/{id}")]
        public IActionResult GetBlogDetail(int id)
        {
            var BlogDetail = _db.Blogdetails.Where(x => x.BlogId == id).ToList();
            if(BlogDetail == null)
            {
                return NotFound();
            }
            return Ok(BlogDetail);

        }
        //[HttpGet("Search")]
        //public IActionResult Search(string keyword)
        //{
        //    if (string.IsNullOrWhiteSpace(keyword))
        //        return BadRequest("Keyword is required");

        //    var blogHeaders = _db.BlogHeaders
        //        .Where(x => EF.Functions.Like(x.BlogTitle, $"%{keyword}%"))
        //        .ToList();

        //    var blogDetails = _db.Blogdetails
        //        .Where(x => EF.Functions.Like(x.BlogContent, $"%{keyword}%"))
        //        .ToList();

        //    var result = new
        //    {
        //        Headers = blogHeaders,
        //        Details = blogDetails
        //    };

        //    return Ok(result);
        //}

        [HttpGet("Search")]
        public IActionResult Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest("Keyword is required");

            var headerResults = _db.BlogHeaders
                .Where(x => EF.Functions.Like(x.BlogTitle, $"%{keyword}%"))
                .Select(x => new
                {
                    Type = "Header",
                    Id = x.BlogId,
                    Text = x.BlogTitle
                });

            var detailResults = _db.Blogdetails
                .Where(x => EF.Functions.Like(x.BlogContent, $"%{keyword}%"))
                .Select(x => new
                {
                    Type = "Detail",
                    Id = x.BlogDetailId,
                    Text = x.BlogContent
                });

            var result = headerResults
                .Union(detailResults)
                .ToList();

            return Ok(result);
        }
        [HttpGet("List")]
        public IActionResult GetBlogList(int pageNo = 1, int pageSize = 10)
        {
            if (pageNo <= 0) pageNo = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _db.BlogHeaders.AsQueryable();

            var totalCount = query.Count();

            var data = query
                .OrderBy(x => x.BlogId)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new
            {
                PageNo = pageNo,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                Data = data
            };

            return Ok(result);
        }

    }
}
