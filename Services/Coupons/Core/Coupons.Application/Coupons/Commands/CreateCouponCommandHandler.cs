using Coupons.Application.Abstractors.Interfaces;
using Coupons.Application.Abstractors.Messages.Handlers;
using Coupons.Domain.Common;
using Coupons.Domain.Entities;
using Coupons.Domain.Shared;
using Coupons.Domain.ValueObjects;

namespace Coupons.Application.Coupons.Commands;

public class CreateCouponCommandHandler(ICouponRepository couponRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateCouponCommand, Guid>
{
    public async Task<ResultT<Guid>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        var couponCode = CouponCode.Create(request.CouponCode);

        if (couponCode.IsFailure)
            return Result.Failure<Guid>(DomainErrors.Coupon.InvalidValue(nameof(couponCode)));

        if (!await couponRepository.IsUniqueCouponCode(couponCode.Value.Value))
        {
            return Result.Failure<Guid>(DomainErrors.Coupon.AlreadyExists(nameof(couponCode)));
        }

        var coupon = Coupon.Create(
            Guid.NewGuid(),
            couponCode.Value,
            request.DiscountAmount,
            request.MinAmount,
            request.CouponValidityPeriod);

        if (coupon.IsFailure)
            return Result.Failure<Guid>(coupon.Error);

        await couponRepository.CreateCoupon(coupon.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(coupon.Value.Id);
    }
}