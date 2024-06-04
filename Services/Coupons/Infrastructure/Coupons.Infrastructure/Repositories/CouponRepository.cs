using Coupons.Application.Abstractors.Interfaces;
using Coupons.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Infrastructure.Repositories;

public sealed class CouponRepository(ApplicationDbContext context) : ICouponRepository
{
    public IQueryable<Coupon> GetCoupons()
    {
        return context.Coupons.AsNoTracking().AsQueryable();
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