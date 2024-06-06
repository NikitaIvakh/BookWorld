using Coupons.Application.Abstractors.Interfaces;
using Coupons.Application.Abstractors.Messages.Handlers;
using Coupons.Domain.Common;
using Coupons.Domain.Shared;

namespace Coupons.Application.Coupons.Commands.Delete;

public class DeleteCouponCommandHandler(ICouponRepository couponRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteCouponCommand, bool>
{
    public async Task<ResultT<bool>> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = await couponRepository.GetCouponAsync(request.Id, cancellationToken);

        if (coupon is null)
            return Result.Failure<bool>(DomainErrors.Coupon.NotFound(request.Id));

        await couponRepository.DeleteCoupon(coupon, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(true);
    }
}