using MsAcceso.Domain.Abstractions;

using MediatR;

namespace MsAcceso.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> 
: IRequestHandler<TQuery, Result<TResponse>>
where TQuery : IQuery<TResponse>
{

}