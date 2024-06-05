using Coupons.Application.Abstractors.Messages.Queries;
using Coupons.Domain.Entities;

namespace Coupons.Application.Coupons.Queries.GetById;

public record GetCouponByIdQuery(Guid Id) : IQuery<GetCouponByIdResponse>;