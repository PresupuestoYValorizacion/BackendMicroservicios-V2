using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.Proyectos.CreateEspecialidadTenant;

public sealed record CreateEspecialidadTenantCommand(
    string Nombre,
    string ProyectoId
    // string Correlativo
) : ICommand<Guid>;