using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.Proyectos.CreateProyectoTenant;

public sealed record CreateProyectoTenantCommand(
    string Nombre
) : ICommand<Guid>;