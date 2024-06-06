namespace Coupons.Application.Coupons.Queries.GetCoupons;

public record GetCouponsResponse
(
    string CouponCode,
    decimal DiscountAmount,
    decimal MinAmount,
    DateTime CreatedDate,
    DateTime CouponValidityPeriod
);