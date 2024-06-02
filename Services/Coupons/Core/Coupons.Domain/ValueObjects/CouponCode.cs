using Coupons.Domain.Primitives;

namespace Coupons.Domain.ValueObjects;

public sealed class CouponCode : ValueObject
{
    private CouponCode(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}