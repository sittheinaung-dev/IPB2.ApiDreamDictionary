using System;
using System.Collections.Generic;

namespace IPB2.ApiDreamDictionary.Database.AppDbContextModels;

public partial class Blogdetail
{
    public int BlogDetailId { get; set; }

    public int BlogId { get; set; }

    public string BlogContent { get; set; } = null!;
}
