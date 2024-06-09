using Coupons.Application.Abstractors.Messages.EventHandlers;
using Coupons.Domain.Entities;
using Coupons.Domain.Primitives;
using Coupons.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Persistence.Idempotent;

public sealed class IdempotentDomainEventHandler<TEvent>(
    ApplicationDbContext dbContext,
    INotificationHandler<TEvent> decorated)
    : IDomainEventHandler<TEvent> where TEvent : IDomainEvent
{
    public async Task Handle(TEvent notification, CancellationToken cancellationToken)
    {
        var consumer = decorated.GetType().Name;

        if (await dbContext.Set<OutboxMessageConsumer>().AnyAsync(key => key.Id == notification.Id && key.Name == consumer, cancellationToken))
            return;

        await decorated.Handle(notification, cancellationToken);
        dbContext.Set<OutboxMessageConsumer>().Add(new OutboxMessageConsumer() { Id = Guid.NewGuid(), Name = consumer });
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}