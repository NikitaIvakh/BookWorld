namespace Coupons.Domain.Common;

public sealed class DomainErrors
{
    internal DomainErrors(string code, string message)
    {
        Code = code;
        Message = message;
    }

    private const string Separator = "||";

    public string Code { get; }

    public string Message { get; }

    public string Serialize()
    {
        return Code + Separator + Message;
    }

    public static DomainErrors Deserialize(string serialized)
    {
        var value = serialized.Split([Separator], StringSplitOptions.RemoveEmptyEntries);

        if (value.Length < 2)
        {
            throw new ArgumentException($"Invalid.error.serialization: '{serialized}'");
        }

        return new DomainErrors(value[0], value[1]);
    }
}

public static class Errors
{
    public static class General
    {
        public static DomainErrors InvalidLength(string? message = null)
        {
            var label = message is null ? string.Empty : $"{message}";
            return new DomainErrors("invalid.length", $"{label} is invalid length");
        }

        public static DomainErrors InvalidValue(string? message = null)
        {
            var label = message is null ? string.Empty : $"{message}";
            return new DomainErrors("invalid.value", $"{label} is invalid");
        }

        public static DomainErrors InvalidDiscountAmount(string? message = null)
        {
            var label = message is null ? string.Empty : $"{message}";
            return new DomainErrors("discountAmount.is.invalid", $"{label} is lower then minAmount");
        }
    }
}