using BookWorld.Frontend.Contracts;
using BookWorld.Frontend.Models.Coupons;
using BookWorld.Frontend.Services.Coupons;
using Mapster;

namespace BookWorld.Frontend.Services;

public sealed class CouponService(ICouponClient couponClient) : ICouponService
{
    public async Task GetCouponsAsync(string? searchCode, string? sortColumn, string? sortType, int? page, int? pageSize)
    {
        await couponClient.GetCouponsAsync(searchCode, sortColumn, sortType, page, pageSize);
    }

    public async Task GetCouponAsync(Guid id)
    {
        await couponClient.GetCouponAsync(id);
    }

    public async Task CreateCouponAsync(CreateCouponViewModel couponViewModel)
    {
        var createCouponRequestMapster = couponViewModel.Adapt<CreateCouponRequest>();
        await couponClient.CreateCouponAsync(createCouponRequestMapster);
    }

    public async Task DeleteCouponAsync(Guid id)
    {
        await couponClient.DeleteCouponAsync(id);
    }
}