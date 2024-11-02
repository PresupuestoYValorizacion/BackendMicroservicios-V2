using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Users.ValidarAccesoMenuTenant;

public sealed record ValidarAccesoMenuTenantQuery : IQuery<bool>
{
    public RolId? UserRolId { get; set; }
    public RolTenantId? RolId { get; set; }
    public string? Url { get; set; }
}