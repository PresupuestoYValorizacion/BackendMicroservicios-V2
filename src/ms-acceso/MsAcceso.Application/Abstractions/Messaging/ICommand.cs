
using MediatR;
using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{

}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{

}

public interface IBaseCommand
{}