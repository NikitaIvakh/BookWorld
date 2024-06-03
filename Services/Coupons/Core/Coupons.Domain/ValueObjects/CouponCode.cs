using Coupons.Domain.Common;
using CSharpFunctionalExtensions;
using ValueObject = Coupons.Domain.Primitives.ValueObject;

namespace Coupons.Domain.ValueObjects;

public sealed class CouponCode : ValueObject
{
    private CouponCode(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public Result<CouponCode, DomainErrors> Create(string value)
    {
        if (value.IsEmpty() || value.Length is > Constraints.MAXIMUM_LENGTH or < Constraints.MINIMUM_LENGTH)
            return Errors.General.InvalidLength(nameof(value.Length));

        return new CouponCode(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}