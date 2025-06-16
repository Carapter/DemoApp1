using System;
using System.Collections.Generic;

namespace DemoApp.Entities;

public partial class Sale
{
    public int SaleId { get; set; }

    public string? ProductArticle { get; set; }

    public int? Partner { get; set; }

    public int? ProductCount { get; set; }

    public DateTime? SaleDate { get; set; }

    public virtual Partner? PartnerNavigation { get; set; }

    public virtual Product? ProductArticleNavigation { get; set; }
}
