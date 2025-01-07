using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.Proyectos.CreateProyectoTenant;

public sealed record CreateEspecialidadTenantRequest(
    string Nombre,
    string ProyectoId

) : ICommand<Guid>;