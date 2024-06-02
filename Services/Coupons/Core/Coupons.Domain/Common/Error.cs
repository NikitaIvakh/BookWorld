namespace Coupons.Domain.Common;

public sealed class Error
{
    internal Error(string code, string message)
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

    public static Error Deserialize(string serialized)
    {
        var value = serialized.Split([Separator], StringSplitOptions.RemoveEmptyEntries);

        if (value.Length < 2)
        {
            throw new ArgumentException($"Invalid.error.serialization: '{serialized}'");
        }

        return new Error(value[0], value[1]);
    }
}

public static class Errors
{
    public static class General
    {
        public static Error InvalidLength(string? message = null)
        {
            var label = message is null ? string.Empty : $"{message}";
            return new Error("invalid.length", $"{label} is invalid length");
        }

        public static Error InvalidValue(string? message = null)
        {
            var label = message is null ? string.Empty : $"{message}";
            return new Error("invalid.value", $"{label} is invalid");
        }

        public static Error InvalidDiscountAmount(string? message = null)
        {
            var label = message is null ? string.Empty : $"{message}";
            return new Error("discountAmount.is.invalid", $"{label} is lower then minAmount");
        }
    }
}