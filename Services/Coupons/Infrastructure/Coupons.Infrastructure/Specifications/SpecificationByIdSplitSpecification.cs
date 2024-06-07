using Coupons.Domain.Entities;

namespace Coupons.Infrastructure.Specifications;

public class SpecificationByIdSplitSpecification : Specification<Coupon>
{
    public SpecificationByIdSplitSpecification(Guid id) : base(coupon => coupon.Id == id)
    {
        AddInclude(coupon => coupon.CouponCode.Value);
        IsSplitQuery = true;
    }
}