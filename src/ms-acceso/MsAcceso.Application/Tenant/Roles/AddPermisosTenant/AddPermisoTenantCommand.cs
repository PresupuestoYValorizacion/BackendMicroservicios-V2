using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Root.Roles.AddPermisos;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Roles.AddPermisosTenant;

public sealed record AddPermisosTenantCommand(
    RolTenantId RolId,
    List<SistemaRequest> SistemasRequest

) : ICommand<Guid>;