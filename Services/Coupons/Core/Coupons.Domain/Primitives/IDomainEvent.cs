using MediatR;

namespace Coupons.Domain.Primitives;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}