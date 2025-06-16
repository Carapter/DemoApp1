using System;
using System.Collections.Generic;

namespace DemoApp.Entities;

public partial class Partner
{
    public int PartnerId { get; set; }

    public string? PartnerName { get; set; }

    public string? Director { get; set; }

    public string? DirectorMail { get; set; }

    public string? DirectorPhone { get; set; }

    public string? PartnerLegalAddress { get; set; }

    public string? PartnerInn { get; set; }

    public int? PartnerRating { get; set; }

    public int? PartnerType { get; set; }

    public virtual PartnerType? PartnerTypeNavigation { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
