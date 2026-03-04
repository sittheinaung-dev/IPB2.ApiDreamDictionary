using System;
using System.Collections.Generic;

namespace IPB2.ApiDreamDictionary.Database.AppDbContextModels;

public partial class BlogHeader
{
    public int BlogId { get; set; }

    public string BlogTitle { get; set; } = null!;
}
