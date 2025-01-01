using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.ProyectosTenant;

namespace MsAcceso.Application.Sgo.Proyectos.DeleteProyectoTenant;

public sealed record DeleteProyectoTenantCommand(
    ProyectoTenantId Id
): ICommand<Guid>;