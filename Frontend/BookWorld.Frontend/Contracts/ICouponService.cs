using BookWorld.Frontend.Models.Coupons;

namespace BookWorld.Frontend.Contracts;

public interface ICouponService
{
    Task GetCouponsAsync(string? searchCode, string? sortColumn, string? sortType, int? page, int? pageSize);

    Task GetCouponAsync(Guid id);

    Task CreateCouponAsync(CreateCouponViewModel couponViewModel);

    Task DeleteCouponAsync(Guid id);
}