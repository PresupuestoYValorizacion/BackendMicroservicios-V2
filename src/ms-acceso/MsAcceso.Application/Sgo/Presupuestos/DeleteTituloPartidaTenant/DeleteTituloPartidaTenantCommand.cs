using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.Presupuestos.DeleteTituloPartidaTenant;

public sealed record DeleteTituloPartidaTenantCommand(
    string Id,
    bool IsTitulo
): ICommand<Guid>;