using IPB2.ApiDreamDictionary.Database.AppDbContextModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
