namespace Coupons.Domain.DomainEvents;

public sealed record CreateCouponRaiseDomainEvent(Guid Id, Guid CouponId) : DomainEvent(Id);