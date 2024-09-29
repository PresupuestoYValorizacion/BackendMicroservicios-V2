using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Roles.GetRolByIdTenant;

public sealed record GetRolByIdTenantQuery : IQuery<RolTenantDto>
{
    public Guid RolId {get; set;}
}