using Coupons.Application.Abstractors.Messages.Queries;

namespace Coupons.Application.Coupons.Queries.GetCoupons;

public record GetCouponsQuery() : IQuery<IEnumerable<GetCouponsResponse>>;