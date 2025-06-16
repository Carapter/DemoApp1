using System.Linq;
namespace DemoApp.Models
{
    public class Partner : Entities.Partner
    {
        public Entities.Partner _partner { get; set; }
        public Partner(Entities.Partner partner)
        {
            _partner = partner;
        }
        public string? PartnerTypeDescription => _partner.PartnerTypeNavigation?.PartnerTypeName;
        public string ContactInfo => $"{_partner.DirectorPhone} | {_partner.DirectorMail}";
        public string RatingDisplay => $"Рейтинг: {_partner.PartnerRating ?? 0}";
        public int TotalProductsSold => _partner.Sales?.Sum(s => s.ProductCount) ?? 0;
        public decimal Discount => CalculatePartnerDiscount(TotalProductsSold);
        public string DiscountDisplay => $"Скидка: {Discount:P0}";
        public static decimal CalculatePartnerDiscount(int totalProductsSold)
        {
            return totalProductsSold switch
            {
                < 10000 => 0,
                < 50000 => 0.05m,
                < 300000 => 0.10m,
                _ => 0.15m
            };
        }
    }
}
