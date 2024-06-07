using Coupons.Domain.Entities;

namespace Coupons.Infrastructure.Specifications;

public class SpecificationListSplitSpecification : Specification<Coupon>
{
    public SpecificationListSplitSpecification(string couponCode) : base(coupon => string.IsNullOrEmpty(couponCode) || ((string)coupon.CouponCode).Contains(couponCode))
    {
        AddInclude(coupon => coupon.CouponCode.Value);
    }
}