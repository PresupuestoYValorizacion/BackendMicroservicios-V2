using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.Presupuestos.CreatePartidaTenant;

public sealed record CreatePartidaTenantCommand(
    string Nombre,
    string PresupuestoEspecialidadTituloId,
    int Nivel,
    string PartidaIdPadre
) : ICommand<Guid>;