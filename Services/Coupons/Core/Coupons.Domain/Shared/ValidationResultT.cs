namespace Coupons.Domain.Shared;

public sealed class ValidationResultT<TValue> : ResultT<TValue>, IValidationResult
{
    private ValidationResultT(Error[] errors) : base(default, false, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }

    public static ValidationResultT<TValue> WithErrors(Error[] errors) => new(errors);
}