
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.UsersTenant;

namespace MsAcceso.Application.Tenant.Users.GetUsersByPaginationTenant;

public sealed record GetUsersByPaginationTenantQuery : PaginationParams, IQuery<PagedResults<UserTenantDto>?>
{

}