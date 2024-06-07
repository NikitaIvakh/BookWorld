using Coupons.Application.Abstractors.Interfaces;
using Coupons.Domain.Entities;
using Coupons.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Infrastructure.Repositories;

public sealed class CouponRepository(ApplicationDbContext context) : ICouponRepository
{
    public IQueryable<Coupon> GetCoupons(CancellationToken token = default)
    {
        return ApplySpecification(new SpecificationListSplitSpecification(string.Empty)).AsNoTracking().AsQueryable();
    }

    public async Task<Coupon?> GetCouponAsync(Guid id, CancellationToken token)
    {
        return await ApplySpecification(new SpecificationByIdSplitSpecification(id)).FirstOrDefaultAsync(token);
    }

    public async Task<bool> IsUniqueCouponCode(string couponCode, CancellationToken token = default)
    {
        return !await context.Coupons.AnyAsync(key => key.CouponCode.Value == couponCode, token);
    }

    public async Task<Coupon> CreateCoupon(Coupon coupon, CancellationToken token = default)
    {
        await context.AddAsync(coupon, token);
        return await Task.FromResult(coupon);
    }

    public async Task<Coupon> DeleteCoupon(Coupon coupon, CancellationToken token = default)
    {
        context.Remove(coupon);
        return await Task.FromResult(coupon);
    }

    private IQueryable<Coupon> ApplySpecification(Specification<Coupon> specification)
    {
        return SpecificationEvaluator.GetQuery(context.Set<Coupon>(), specification);
    }
}