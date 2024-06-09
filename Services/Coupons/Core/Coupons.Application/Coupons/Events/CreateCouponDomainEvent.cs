using Coupons.Application.Abstractors.Messages.EventHandlers;
using Coupons.Domain.DomainEvents;

namespace Coupons.Application.Coupons.Events;

public sealed class CreateCouponDomainEvent : IDomainEventHandler<CreateCouponRaiseDomainEvent>
{
    public Task Handle(CreateCouponRaiseDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}