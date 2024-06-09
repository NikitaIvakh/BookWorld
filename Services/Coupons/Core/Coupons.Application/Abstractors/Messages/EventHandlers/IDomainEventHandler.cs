using Coupons.Domain.Primitives;
using MediatR;

namespace Coupons.Application.Abstractors.Messages.EventHandlers;

public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent
{

}