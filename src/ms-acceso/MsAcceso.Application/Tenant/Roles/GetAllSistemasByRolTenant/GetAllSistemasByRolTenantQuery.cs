using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Tenant.Roles.GetAllSistemasByRolTenant;

public sealed record GetAllSistemasByRolTenantQuery : IQuery<List<SistemaByRolDto>>
{
    public string? RolId { get; set; }
    public string? UserRolId { get; set; }

}