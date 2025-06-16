using System;
using System.Collections.Generic;

namespace DemoApp.Entities;

public partial class Product
{
    public string ProductArticle { get; set; } = null!;

    public string? ProductName { get; set; }

    public decimal? MinimumCostForPartner { get; set; }

    public int? ProductType { get; set; }

    public virtual ICollection<ProductMaterialType> ProductMaterialTypes { get; set; } = new List<ProductMaterialType>();

    public virtual ProductType? ProductTypeNavigation { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
