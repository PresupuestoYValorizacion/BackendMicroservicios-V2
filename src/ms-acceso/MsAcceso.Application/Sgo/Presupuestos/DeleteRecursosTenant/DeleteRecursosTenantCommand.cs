using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.Presupuestos.DeleteRecursosTenant;

public sealed record DeleteRecursosTenantCommand(
    string Id
) : ICommand<Guid>;