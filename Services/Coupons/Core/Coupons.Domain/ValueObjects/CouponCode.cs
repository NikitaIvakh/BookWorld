using Coupons.Domain.Common;
using Coupons.Domain.Primitives;
using Coupons.Domain.Shared;

namespace Coupons.Domain.ValueObjects;

public sealed class CouponCode : ValueObject
{
    private CouponCode(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static ResultT<CouponCode> Create(string value)
    {
        if (value.IsEmpty() || value.Length is > Constraints.MAXIMUM_LENGTH or < Constraints.MINIMUM_LENGTH)
            return Result.Failure<CouponCode>(DomainErrors.Coupon.InvalidLength(nameof(value), value.Length));

        var couponCode = new CouponCode(value);
        return Result.Create(couponCode);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}