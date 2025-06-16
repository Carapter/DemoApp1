using System;
using System.Collections.Generic;

namespace DemoApp.Entities;

public partial class MaterialType
{
    public int MaterialTypeId { get; set; }

    public string? MaterialTypeName { get; set; }

    public decimal? PercentageDefectiveMaterial { get; set; }

    public virtual ICollection<ProductMaterialType> ProductMaterialTypes { get; set; } = new List<ProductMaterialType>();
}
