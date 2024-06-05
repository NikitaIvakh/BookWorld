using Coupons.Application.Abstractors.Interfaces;
using Coupons.Application.Abstractors.Messages.Queries;
using Coupons.Domain.Common;
using Coupons.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Application.Coupons.Queries.GetById;

public sealed class GetCouponByIdQueryHandler(ICouponRepository couponRepository)
    : IQueryHandler<GetCouponByIdQuery, GetCouponByIdResponse>
{
    public async Task<ResultT<GetCouponByIdResponse>> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
    {
        var coupon = await couponRepository.GetCoupons().FirstOrDefaultAsync(key => key.Id == request.Id, cancellationToken);

        if (coupon is null)
            return Result.Failure<GetCouponByIdResponse>(DomainErrors.Coupon.NotFound(request.Id));

        var couponResponse = new GetCouponByIdResponse(coupon.CouponCode.Value, coupon.DiscountAmount, coupon.MinAmount,
            coupon.CreatedDate, coupon.CouponValidityPeriod);

        return Result.Success(couponResponse);
    }
}