using Coupons.Domain.Entities;
using Coupons.Domain.Primitives;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Coupons.Infrastructure.Interceptors;

public sealed class ConvertDomainEventToOutboxMessageInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var context = eventData.Context;

        if (context is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        var outboxMessage = context.ChangeTracker.Entries<AggregateRoot>().Select(key => key.Entity).SelectMany(aggregateRoot =>
        {
            var domainEvents = aggregateRoot.GetDomainEvents();
            aggregateRoot.ClearDomainEvents();
            return domainEvents;
        })
            .Select(domainEvent => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                }),
                OccurredOnUtc = DateTime.UtcNow,
            });

        context.Set<OutboxMessage>().AddRange(outboxMessage);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}