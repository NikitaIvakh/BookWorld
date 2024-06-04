using Coupons.Domain.Shared;
using MediatR;

namespace Coupons.Application.Abstractors.Messages.Handlers;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result> where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, ResultT<TResponse>>
    where TCommand : ICommand<TResponse>;