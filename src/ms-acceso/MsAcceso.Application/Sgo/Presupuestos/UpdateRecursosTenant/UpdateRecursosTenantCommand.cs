using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.Presupuestos.UpdateRecursosTenant;

public sealed record UpdateRecursosTenantCommand(
    string Id,
    string PartidaId,
    string RecursoId,
    int Cantidad,
    int Cuadrilla,
    double Precio
) : ICommand<Guid>;