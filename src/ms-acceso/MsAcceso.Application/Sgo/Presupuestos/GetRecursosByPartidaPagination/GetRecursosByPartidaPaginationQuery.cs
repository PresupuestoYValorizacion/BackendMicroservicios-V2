using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.GetRecursosByPartidaPagination;

public sealed record GetRecursosByPartidaPaginationQuery : PaginationParams, IQuery<PagedResults<ClienteDto>?>
{

}