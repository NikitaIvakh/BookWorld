using Coupons.Domain.Common;
using Coupons.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coupons.Infrastructure.Configurations;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.HasKey(key => key.Id);
        builder.Property(key => key.Id).ValueGeneratedOnAdd();

        builder.ComplexProperty(key => key.CouponCode, b =>
        {
            b.Property(key => key.Value).HasMaxLength(Constraints.MAXIMUM_LENGTH).IsRequired();
        });

        builder.Property(key => key.DiscountAmount).IsRequired();
        builder.Property(key => key.MinAmount).IsRequired();
        builder.Property(key => key.CouponValidityPeriod).IsRequired();
    }
}