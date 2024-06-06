using Coupons.Application.Abstractors.Messages.Handlers;

namespace Coupons.Application.Coupons.Commands.Delete;

public record DeleteCouponCommand(Guid Id) : ICommand<bool>;