using Coupons.Domain.Shared;

namespace Coupons.Domain.Common;

public static class DomainErrors
{
    public static class Coupon
    {
        public static readonly Func<string, Error> InvalidLength = value =>
            new Error("invalid.length", $"{value} has a invalid length");

        public static readonly Func<string, Error> InvalidValue = value =>
            new Error("value.is.invalid", $"{value} is invalid");

        public static readonly Func<string, Error> InvalidDiscountAmount =
            value => new Error("discountAmount.is.invalid", $"{value} is lower then MinAmount");
    }
}