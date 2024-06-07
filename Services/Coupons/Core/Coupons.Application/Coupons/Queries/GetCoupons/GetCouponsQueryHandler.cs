using Coupons.Application.Abstractors.Interfaces;
using Coupons.Application.Abstractors.Messages.Queries;
using Coupons.Domain.Common;
using Coupons.Domain.Entities;
using Coupons.Domain.Shared;
using System.Linq.Expressions;

namespace Coupons.Application.Coupons.Queries.GetCoupons;

public class GetCouponsQueryHandler(ICouponRepository couponRepository)
    : IQueryHandler<GetCouponsQuery, PaginationList<GetCouponsResponse>>
{
    public async Task<ResultT<PaginationList<GetCouponsResponse>>> Handle(GetCouponsQuery request, CancellationToken cancellationToken)
    {
        var coupons = couponRepository.GetCoupons(cancellationToken);

        if (coupons is null)
            return Result.Failure<PaginationList<GetCouponsResponse>>(DomainErrors.Coupon.CollectionNotFound(nameof(coupons)));

        if (!string.IsNullOrEmpty(request.SearchCode))
        {
            coupons = coupons.Where(key => ((string)key.CouponCode).Contains(request.SearchCode));
        }

        coupons = request.SortType?.ToLower() == "desc"
            ? coupons.OrderByDescending(GetSortedProperty(request))
            : coupons.OrderBy(GetSortedProperty(request));

        var couponsResponse = coupons.Select(key => new GetCouponsResponse
        (
            key.CouponCode.Value,
            key.DiscountAmount,
            key.MinAmount,
            key.CreatedDate,
            key.CouponValidityPeriod
        )).ToList();

        var couponsResponsePagination = await PaginationList<GetCouponsResponse>.CreateAsync(couponsResponse, request.Page, request.PageSize);
        return Result.Success(couponsResponsePagination);
    }

    private static Expression<Func<Coupon, object>> GetSortedProperty(GetCouponsQuery request)
    {
        return request.SortColumn?.ToLower() switch
        {
            "couponcode" => coupon => coupon.CouponCode,
            "discountamount" => coupon => coupon.DiscountAmount,
            "minamount" => coupon => coupon.MinAmount,
            "createddate" => coupon => coupon.CreatedDate,
            "couponvalidityperiod" => coupon => coupon.CouponValidityPeriod,
            _ => coupon => coupon.CreatedDate
        };
    }
}