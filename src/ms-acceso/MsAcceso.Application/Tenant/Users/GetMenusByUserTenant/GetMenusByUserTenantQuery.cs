using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Tenant.Users.GetMenusByUserTenant;

public sealed record GetMenusByUserTenantQuery : IQuery<List<SistemaByRolDto>>
{
    public string? RolId { get; set; }
    public string? UserTenantRolId { get; set; }
    public string? Dependencia { get; set; }
}