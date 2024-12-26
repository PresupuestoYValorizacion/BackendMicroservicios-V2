using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.CarpetasPresupuestales.CreateCarpetaPresupuestal;

public sealed record CreateCarpetaPresupuestalCommand(
    string Dependencia,
    string Nombre,
    int Nivel
) : ICommand<Guid>;