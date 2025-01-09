using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.Presupuestos.CreateTituloTenant;

public sealed record CreateTituloTenantCommand(
    string Nombre,
    string EspecialidadId
) : ICommand<Guid>;