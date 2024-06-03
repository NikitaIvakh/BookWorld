using Coupons.Domain.Common;
using Coupons.Domain.Primitives;
using Coupons.Domain.Shared;
using Coupons.Domain.ValueObjects;

namespace Coupons.Domain.Entities;

public sealed class Coupon : Entity, IAuditableEntity
{
    private Coupon()
    {
    }

    private Coupon(Guid id, CouponCode couponCode, decimal discountAmount, decimal minAmount, DateTime couponValidityPeriod) : base(id)
    {
        CouponCode = couponCode;
        DiscountAmount = discountAmount;
        MinAmount = minAmount;
        CouponValidityPeriod = couponValidityPeriod;
    }

    public CouponCode CouponCode { get; private set; }

    public decimal DiscountAmount { get; private set; }

    public decimal MinAmount { get; private set; }

    public DateTime CreatedDate { get; set; }

    public DateTime CouponValidityPeriod { get; private set; }

    public ResultT<Coupon> Create(Guid id, CouponCode couponCode, decimal discountAmount, decimal minAmount, DateTime couponValidityPeriod)
    {
        if (discountAmount is > Constraints.MAX_VALUE or < Constraints.MIN_VALUE)
            return Result.Failure<Coupon>(DomainErrors.Coupon.InvalidValue(nameof(discountAmount)));

        if (minAmount is > Constraints.MAX_VALUE or < Constraints.MIN_VALUE)
            return Result.Failure<Coupon>(DomainErrors.Coupon.InvalidValue(nameof(minAmount)));

        if (minAmount > discountAmount)
            return Result.Failure<Coupon>(DomainErrors.Coupon.InvalidDiscountAmount(nameof(discountAmount)));

        var coupon = new Coupon(id, couponCode, discountAmount, minAmount, couponValidityPeriod);
        return Result.Create(coupon);
    }
}