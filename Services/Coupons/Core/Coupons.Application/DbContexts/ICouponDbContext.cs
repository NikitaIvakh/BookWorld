using Coupons.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Application.DbContexts;

public interface ICouponDbContext
{
    public DbSet<Coupon> Coupons { get; }

    Task<int> SaveChangesAsync(CancellationToken token = default);
}