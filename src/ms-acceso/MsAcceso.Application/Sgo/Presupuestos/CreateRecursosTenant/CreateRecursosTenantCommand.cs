using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.Presupuestos.CreateRecursosTenant;

public sealed record CreateRecursosTenantCommand(
    string PartidaId,
    string RecursoId,
    int Cantidad,
    int Cuadrilla,
    double Precio,
    string TipoCuadrilla
) : ICommand<Guid>;