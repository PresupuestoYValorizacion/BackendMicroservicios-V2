
using MediatR;
using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    
}