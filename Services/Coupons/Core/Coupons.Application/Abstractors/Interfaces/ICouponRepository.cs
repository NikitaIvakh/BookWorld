using Coupons.Domain.Entities;

namespace Coupons.Application.Abstractors.Interfaces;

public interface ICouponRepository
{
    Task<Coupon?> GetCouponAsync(Guid id, CancellationToken token);

    Task<bool> IsUniqueCouponCode(string couponCode, CancellationToken token = default);

    Task<Coupon> CreateCoupon(Coupon coupon, CancellationToken token = default);

    Task<Coupon> DeleteCoupon(Coupon coupon, CancellationToken token = default);
}