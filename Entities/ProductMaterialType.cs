using System;
using System.Collections.Generic;

namespace DemoApp.Entities;

public partial class ProductMaterialType
{
    public int ProductMaterialTypeId { get; set; }

    public string? Product { get; set; }

    public int? MaterialType { get; set; }

    public virtual MaterialType? MaterialTypeNavigation { get; set; }

    public virtual Product? ProductNavigation { get; set; }
}
