namespace IPB2.ApiDreamDictionaryMVC.Controllers.BlogHeader;

public class BlogHeaderRequest
{
    public int BlogId { get; set; }
    public string BlogTitle { get; set; } = null!;
}

public class BlogHeaderResponse
{
    public int BlogId { get; set; }
    public string BlogTitle { get; set; } = null!;
}
