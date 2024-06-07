using Coupons.Application.Abstractors.Interfaces;
using Coupons.Application.Abstractors.Messages.Queries;
using Coupons.Domain.Shared;

namespace Coupons.Application.Coupons.Queries.GetCoupons;

public class GetCouponsQueryHandler(ICouponRepository couponRepository)
    : IQueryHandler<GetCouponsQuery, PaginationList<GetCouponsResponse>>
{
    public async Task<ResultT<PaginationList<GetCouponsResponse>>> Handle(GetCouponsQuery request, CancellationToken cancellationToken)
    {
        var coupons = await couponRepository.GetCouponsAsync(cancellationToken);

        if (!string.IsNullOrEmpty(request.SearchCode))
        {
            coupons = coupons.Where(key => ((string)key.CouponCode).Contains(request.SearchCode));
        }

        coupons = request.SortType?.ToLower() == "desc"
            ? coupons.OrderByDescending(key => key.CreatedDate)
            : coupons.OrderBy(key => key.CreatedDate);

        var couponsResponse = coupons.Select(key => new GetCouponsResponse
        (
            key.CouponCode.Value,
            key.DiscountAmount,
            key.MinAmount,
            key.CreatedDate,
            key.CouponValidityPeriod
        )).ToList();

        return Result.Success(await PaginationList<GetCouponsResponse>.CreateAsync(couponsResponse, request.Page, request.PageSize));
    }
}