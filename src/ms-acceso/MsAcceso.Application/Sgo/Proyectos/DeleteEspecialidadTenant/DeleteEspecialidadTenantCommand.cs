using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;

namespace MsAcceso.Application.Sgo.Proyectos.DeleteEspecialidadTenant;

public sealed record DeleteEspecialidadTenantCommand(
    EspecialidadTenantId Id
): ICommand<Guid>;