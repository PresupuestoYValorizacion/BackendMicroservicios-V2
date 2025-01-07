using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.Proyectos.UpdateProyectoTenant;

public sealed record UpdateProyectoTenantCommand(
    string Id,
    string Nombre,
    bool IsProyecto
): ICommand<Guid>;