using Coupons.Domain.Shared;

namespace Coupons.Domain.Common;

public static class DomainErrors
{
    public static class Coupon
    {
        public static readonly Func<string, int, Error> InvalidLength = (property, length) =>
            new Error("invalid.length", $"The property {property} has an invalid length of {length}");

        public static readonly Func<string, Error> InvalidValue = value =>
            new Error("value.is.invalid", $"{value} is invalid");

        public static readonly Func<string, Error> InvalidDiscountAmount =
            value => new Error("discountAmount.is.invalid", $"{value} is higher then MinAmount");

        public static readonly Func<string, Error> AlreadyExists = value =>
            new Error("already.exists", $"{value} is already exists");

        public static readonly Func<string, Error> InvalidDateTime =
            value => new Error("datetime.is.invalid", $"{value} is invalid");
    }
}