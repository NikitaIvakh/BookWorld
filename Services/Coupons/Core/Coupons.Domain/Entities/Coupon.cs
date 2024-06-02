namespace Coupons.Domain.Entities;

public sealed class Coupon
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string CouponCode { get; set; } = string.Empty;

    public string DiscountAmount { get; set; } = string.Empty;

    public string MinAmount { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public DateTime CouponValidityPeriod { get; set; }
}