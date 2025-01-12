using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.Presupuestos.UpdatePartidaTenant;

public sealed record UpdatePartidaTenantCommand(
    string Id,
    string Nombre

) : ICommand<Guid>;