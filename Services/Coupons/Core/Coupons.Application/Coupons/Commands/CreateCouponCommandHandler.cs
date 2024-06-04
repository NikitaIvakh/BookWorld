using Coupons.Application.Abstractors.Interfaces;
using Coupons.Application.Abstractors.Messages.Handlers;
using Coupons.Domain.Entities;
using Coupons.Domain.Shared;
using Coupons.Domain.ValueObjects;

namespace Coupons.Application.Coupons.Commands;

public class CreateCouponCommandHandler(ICouponRepository couponRepository) : ICommandHandler<CreateCouponCommand, Guid>
{
    public async Task<ResultT<Guid>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        var couponCode = CouponCode.Create(request.CouponCode).Value;
        var coupon = Coupon.Create(Guid.NewGuid(), couponCode, request.DiscountAmount, request.MinAmount,
            request.CouponValidityPeriod).Value;

        await couponRepository.CreateCoupon(coupon, cancellationToken);
        return Result.Success(coupon.Id);
    }
}