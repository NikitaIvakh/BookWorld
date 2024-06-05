using Coupons.Application.Abstractors.Interfaces;
using Coupons.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Infrastructure.Repositories;

public sealed class CouponRepository(ApplicationDbContext context) : ICouponRepository
{
    public async Task<Coupon?> GetCouponAsync(Guid id, CancellationToken token)
    {
        return await context.Coupons.FirstOrDefaultAsync(key => key.Id == id, token);
    }

    public async Task<bool> IsUniqueCouponCode(string couponCode, CancellationToken token = default)
    {
        return !await context.Coupons.AnyAsync(key => key.CouponCode.Value == couponCode, token);
    }

    public async Task<Coupon> CreateCoupon(Coupon coupon, CancellationToken token = default)
    {
        if (coupon is null)
        {
            throw new ArgumentNullException(nameof(coupon));
        }

        await context.AddAsync(coupon, token);
        return await Task.FromResult(coupon);
    }

    public async Task<Coupon> DeleteCoupon(Coupon coupon, CancellationToken token = default)
    {
        if (coupon is null)
        {
            throw new ArgumentNullException(nameof(coupon));
        }

        context.Remove(coupon);
        return await Task.FromResult(coupon);
    }
}