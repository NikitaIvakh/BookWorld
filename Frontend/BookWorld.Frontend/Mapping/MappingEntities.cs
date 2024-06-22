using BookWorld.Frontend.Models.Coupons;
using BookWorld.Frontend.Services.Coupons;
using Mapster;

namespace BookWorld.Frontend.Mapping;

public sealed class MappingEntities
{
    public MappingEntities()
    {
        TypeAdapterConfig<CreateCouponRequest, CreateCouponViewModel>.NewConfig()
            .Map(dest => dest.CouponCode, src => src.CouponCode)
            .Map(dest => dest.DiscountAmount, src => src.DiscountAmount)
            .Map(dest => dest.MinAmount, src => src.MinAmount);
    }
}