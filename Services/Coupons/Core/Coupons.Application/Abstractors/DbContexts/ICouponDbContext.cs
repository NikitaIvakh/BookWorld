using Coupons.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Application.Abstractors.DbContexts;

public interface ICouponDbContext
{
    public DbSet<Coupon> Coupons { get; }

    public DbSet<OutboxMessage> OutboxMessages { get; }

    public DbSet<OutboxMessageConsumer> OutboxMessagesConsumer { get; }

    Task<int> SaveChangesAsync(CancellationToken token = default);
}