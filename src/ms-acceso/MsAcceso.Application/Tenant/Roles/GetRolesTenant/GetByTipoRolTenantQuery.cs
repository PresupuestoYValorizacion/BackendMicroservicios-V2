using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Roles.GetRolesTenant;

public sealed record GetRolesTenantQuery : IQuery<List<RolTenantDto>>
{
}