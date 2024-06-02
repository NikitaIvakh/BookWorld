using Coupons.Domain.Primitives;

namespace Coupons.Domain.Entities;

public sealed class Coupon : Entity
{
    private Coupon()
    {
    }

    private Coupon(Guid id) : base(id)
    {
    }

    public string CouponCode { get; set; } = string.Empty;

    public string DiscountAmount { get; set; } = string.Empty;

    public string MinAmount { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public DateTime CouponValidityPeriod { get; set; }
}