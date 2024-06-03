using Coupons.Domain.Common;
using Coupons.Domain.Primitives;
using Coupons.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Entity = Coupons.Domain.Primitives.Entity;

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

    public Result<Coupon, DomainErrors> Create(Guid id, CouponCode couponCode, decimal discountAmount, decimal minAmount, DateTime couponValidityPeriod)
    {
        if (discountAmount is > Constraints.MAX_VALUE or < Constraints.MIN_VALUE)
            return Errors.General.InvalidValue(nameof(discountAmount));

        if (minAmount is > Constraints.MAX_VALUE or < Constraints.MIN_VALUE)
            return Errors.General.InvalidValue(nameof(minAmount));

        if (minAmount > discountAmount)
            return Errors.General.InvalidDiscountAmount(nameof(discountAmount));

        return new Coupon(id, couponCode, discountAmount, minAmount, couponValidityPeriod);
    }
}