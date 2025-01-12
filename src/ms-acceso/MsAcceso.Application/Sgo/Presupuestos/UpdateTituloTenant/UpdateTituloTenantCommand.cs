using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.Presupuestos.UpdateTituloTenant;

public sealed record UpdateTituloTenantCommand(
    string Id,
    string Nombre
) : ICommand<Guid>;