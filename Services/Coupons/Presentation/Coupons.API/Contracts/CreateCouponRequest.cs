namespace Coupons.API.Contracts;

public record CreateCouponRequest(string CouponCode, decimal DiscountAmount, decimal MinAmount, DateTime CouponValidityPeriod);