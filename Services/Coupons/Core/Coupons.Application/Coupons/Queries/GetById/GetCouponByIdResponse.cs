namespace Coupons.Application.Coupons.Queries.GetById;

public record GetCouponByIdResponse
(
    string CouponCode,
    decimal DiscountAmount,
    decimal MinAmount,
    DateTime CreatedDate,
    DateTime CouponValidityPeriod
);