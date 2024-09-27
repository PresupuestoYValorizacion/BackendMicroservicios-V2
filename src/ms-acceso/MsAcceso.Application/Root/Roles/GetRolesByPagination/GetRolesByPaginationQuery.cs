using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Root.Roles.GetRolesByPagination;

public sealed record GetRolesByPaginationQuery : PaginationParams, IQuery<PagedResults<RolDto>?>
{

}