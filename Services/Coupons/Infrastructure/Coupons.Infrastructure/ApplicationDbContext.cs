using Coupons.Application.Abstractors.DbContexts;
using Coupons.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), ICouponDbContext
{
    public DbSet<Coupon> Coupons { get; private init; }

    public DbSet<OutboxMessage> OutboxMessages { get; private set; }

    public DbSet<OutboxMessageConsumer> OutboxMessagesConsumer { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}