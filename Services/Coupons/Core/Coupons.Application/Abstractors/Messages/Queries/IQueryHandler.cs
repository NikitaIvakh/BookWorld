using Coupons.Domain.Shared;
using MediatR;

namespace Coupons.Application.Abstractors.Messages.Queries;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, ResultT<TResponse>> where TQuery : IQuery<TResponse>;