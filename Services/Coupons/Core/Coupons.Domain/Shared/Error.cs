namespace Coupons.Domain.Shared;

public sealed class Error(string code, string message) : IEquatable<Error>
{
    public static readonly Error None = new("string.is.empty", "string.is.empty");
    public static readonly Error NullValue = new("DomainErrors.NullValue", "The specified result value is null");

    public string Code { get; } = code;

    public string Message { get; } = message;

    public static bool operator ==(Error? left, Error? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Error? left, Error? right)
    {
        return !(left == right);
    }

    public bool Equals(Error? other)
    {
        if (other is null)
            return false;

        if (GetType() != other.GetType())
            return false;

        return Code == other.Code && Message == other.Message;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (GetType() != obj.GetType())
            return false;

        return obj is Error error && Equals(error);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Message);
    }

    public override string ToString()
    {
        return $"{Code}: {Message}";
    }
}