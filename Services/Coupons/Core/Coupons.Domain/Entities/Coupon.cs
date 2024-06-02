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

    public string CouponCode { get; private set; } = string.Empty;

    public decimal DiscountAmount { get; private set; }

    public decimal MinAmount { get; private set; }

    public DateTime CreatedDate { get; private set; }

    public DateTime CouponValidityPeriod { get; private set; }
}