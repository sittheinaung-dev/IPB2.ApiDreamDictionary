namespace IPB2.ApiDreamDictionaryMVC.Controllers.BlogDetail;

public class BlogDetailRequest
{
    public int BlogDetailId { get; set; }
    public int BlogId { get; set; }
    public string BlogContent { get; set; } = null!;
}

public class BlogDetailResponse
{
    public int BlogDetailId { get; set; }
    public int BlogId { get; set; }
    public string BlogContent { get; set; } = null!;
    public string? BlogTitle { get; set; } 
}
