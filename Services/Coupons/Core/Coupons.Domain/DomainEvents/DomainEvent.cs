using Coupons.Domain.Primitives;

namespace Coupons.Domain.DomainEvents;

public abstract record DomainEvent(Guid Id) : IDomainEvent;