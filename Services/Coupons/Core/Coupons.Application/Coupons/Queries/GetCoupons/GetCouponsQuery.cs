using Coupons.Application.Abstractors.Messages.Queries;

namespace Coupons.Application.Coupons.Queries.GetCoupons;

public record GetCouponsQuery(string? SearchCode, string? SortColumn, string? SortType) : IQuery<IEnumerable<GetCouponsResponse>>;