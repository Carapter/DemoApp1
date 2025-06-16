using System;
using System.Collections.Generic;

namespace DemoApp.Entities;

public partial class PartnerType
{
    public int PartnerTypeId { get; set; }

    public string? PartnerTypeName { get; set; }

    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();
}
