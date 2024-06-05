using Coupons.Domain.Shared;
using MediatR;

namespace Coupons.Application.Abstractors.Messages.Queries;

public interface IQuery<TResponse> : IRequest<ResultT<TResponse>>;