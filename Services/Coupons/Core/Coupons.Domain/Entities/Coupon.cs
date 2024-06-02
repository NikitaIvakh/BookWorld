using Coupons.Domain.Common;
using Coupons.Domain.Primitives;
using CSharpFunctionalExtensions;
using Entity = Coupons.Domain.Primitives.Entity;

namespace Coupons.Domain.Entities;

public sealed class Coupon : Entity, IAuditableEntity
{
    private Coupon()
    {
    }

    private Coupon(Guid id, string couponCode, decimal discountAmount, decimal minAmount) : base(id)
    {
        CouponCode = couponCode;
        DiscountAmount = discountAmount;
        MinAmount = minAmount;
    }

    public string CouponCode { get; private set; } = string.Empty;

    public decimal DiscountAmount { get; }

    public decimal MinAmount { get; }

    public DateTime CreatedDate { get; set; }

    public DateTime CouponValidityPeriod { get; set; }

    public Result<Coupon, Error> Create(Guid id, string couponCode, decimal discountAmount, decimal minAmount)
    {
        if (couponCode.IsEmpty())
            return Errors.General.InvalidLength(nameof(couponCode.Length));

        return new Coupon(id, couponCode, discountAmount, minAmount);
    }
}