using Coupons.Domain.Shared;
using MediatR;

namespace Coupons.Application.Abstractors.Messages.Handlers;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<ResultT<TResponse>>;