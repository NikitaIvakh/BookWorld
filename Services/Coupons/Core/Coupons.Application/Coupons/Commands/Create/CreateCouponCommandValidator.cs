using Coupons.Domain.Common;
using FluentValidation;

namespace Coupons.Application.Coupons.Commands.Create;

public class CreateCouponCommandValidator : AbstractValidator<CreateCouponCommand>
{
    public CreateCouponCommandValidator()
    {
        RuleFor(key => key.CouponCode).NotEmpty().MaximumLength(Constraints.MAXIMUM_LENGTH);
        RuleFor(key => key.DiscountAmount).NotEmpty();
        RuleFor(key => key.MinAmount).NotEmpty();
        RuleFor(key => key.CouponValidityPeriod).NotEmpty();
    }
}