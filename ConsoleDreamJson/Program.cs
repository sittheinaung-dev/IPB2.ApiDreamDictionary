
using IPB2.ApiDreamDictionary.Database.AppDbContextModels;
using Newtonsoft.Json;
using System;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;


Console.WriteLine("Console Dream Json");
AppDbContext db = new AppDbContext();

var blogHeaderJson = File.ReadAllText("DreamDictionary.json");
BlogHeaders blogHeaders = JsonConvert.DeserializeObject<BlogHeaders>(blogHeaderJson);

foreach (BlogheaderItem header in blogHeaders.BlogHeader)
{
    var ExistingHeader = db.BlogHeaders
                       .FirstOrDefault(x => x.BlogId == header.BlogId);

    if(ExistingHeader==null)
    {
        var blogHeader = new BlogHeader()
        {
            BlogId = header.BlogId,
            BlogTitle = header.BlogTitle
        };
        db.BlogHeaders.Add(blogHeader);
        db.SaveChanges();
    }

}

var blogDetailJson = File.ReadAllText("DreamDictionary.json");
BlogDetails blogDetails = JsonConvert.DeserializeObject<BlogDetails>(blogDetailJson);

foreach (BlogdetailItem detail in blogDetails.BlogDetail)
{
    var blogHeader = db.BlogHeaders
                       .FirstOrDefault(x => x.BlogId == detail.BlogId);
    if(blogHeader!=null)
    {
        var existingDetail = db.Blogdetails.FirstOrDefault(x => x.BlogDetailId == detail.BlogDetailId);
        if(existingDetail==null)
        {
            var blogDetail = new Blogdetail()
            {
                BlogDetailId = detail.BlogDetailId,
                BlogId = blogHeader.BlogId,
                BlogContent = detail.BlogContent

            };

            db.Blogdetails.Add(blogDetail);
            db.SaveChanges();
        }
    }

   
}
Console.WriteLine("Insert Successful!");
Console.ReadLine();

public class BlogHeaders
{
    public BlogheaderItem[] BlogHeader { get; set; }
}
public class BlogheaderItem
{
    public int BlogId { get; set; }
    public string BlogTitle { get; set; }
}
public class BlogDetails
{
    public BlogdetailItem[] BlogDetail { get; set; }
}
public class BlogdetailItem
{
    public int BlogDetailId { get; set; }
    public int BlogId { get; set; }
    public string BlogContent { get; set; }
}
