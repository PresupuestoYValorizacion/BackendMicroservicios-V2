using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Application.Sgo.Clientes.GetClienteByPagination;

public sealed record GetClienteByPaginationQuery : PaginationParams, IQuery<PagedResults<ClienteDto>?>
{

}