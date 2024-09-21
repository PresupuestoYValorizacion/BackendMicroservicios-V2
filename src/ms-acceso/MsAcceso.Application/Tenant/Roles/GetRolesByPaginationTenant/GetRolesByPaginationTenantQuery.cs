using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Roles.GetRolesByPaginationTenant;

public sealed record GetRolesByPaginationTenantQuery : PaginationParams, IQuery<PagedResults<RolTenantDto>?>
{

}