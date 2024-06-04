using Coupons.Application.Abstractors.Messages.Handlers;

namespace Coupons.Application.Coupons.Commands;

public sealed record CreateCouponCommand
(
    string CouponCode,
    decimal DiscountAmount,
    decimal MinAmount,
    DateTime CouponValidityPeriod
) : ICommand<Guid>;