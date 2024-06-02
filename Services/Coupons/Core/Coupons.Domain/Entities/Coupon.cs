using Coupons.Domain.Primitives;
using Coupons.Domain.ValueObjects;
using Entity = Coupons.Domain.Primitives.Entity;

namespace Coupons.Domain.Entities;

public sealed class Coupon : Entity, IAuditableEntity
{
    private Coupon()
    {
    }

    private Coupon(Guid id, CouponCode couponCode, decimal discountAmount, decimal minAmount) : base(id)
    {
        CouponCode = couponCode;
        DiscountAmount = discountAmount;
        MinAmount = minAmount;
    }

    public CouponCode CouponCode { get; private set; }

    public decimal DiscountAmount { get; }

    public decimal MinAmount { get; }

    public DateTime CreatedDate { get; set; }

    public DateTime CouponValidityPeriod { get; set; }
}