using Coupons.Application.Abstractors.Interfaces;
using Coupons.Application.Abstractors.Messages.Queries;
using Coupons.Domain.Common;
using Coupons.Domain.Shared;

namespace Coupons.Application.Coupons.Queries.GetCoupons;

public class GetCouponsQueryHandler(ICouponRepository couponRepository)
    : IQueryHandler<GetCouponsQuery, IEnumerable<GetCouponsResponse>>
{
    public async Task<ResultT<IEnumerable<GetCouponsResponse>>> Handle(GetCouponsQuery request, CancellationToken cancellationToken)
    {
        var coupons = await couponRepository.GetCouponsAsync(cancellationToken);

        if (!coupons.Any())
            return Result.Failure<IEnumerable<GetCouponsResponse>>(DomainErrors.Coupon.CollectionNotFound(nameof(coupons)));

        var couponsResponse = coupons.Select(key => new GetCouponsResponse
        (
            key.CouponCode.Value,
            key.DiscountAmount,
            key.MinAmount,
            key.CreatedDate,
            key.CouponValidityPeriod
        ));

        return Result.Success(couponsResponse);
    }
}