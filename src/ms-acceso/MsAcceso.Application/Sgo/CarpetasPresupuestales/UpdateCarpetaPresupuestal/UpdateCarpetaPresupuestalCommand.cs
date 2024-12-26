using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.CarpetasPresupuestales.UpdateCarpetaPresupuestal;

public sealed record UpdateCarpetaPresupuestalCommand(
    string Id,
    string Nombre
): ICommand<Guid>;