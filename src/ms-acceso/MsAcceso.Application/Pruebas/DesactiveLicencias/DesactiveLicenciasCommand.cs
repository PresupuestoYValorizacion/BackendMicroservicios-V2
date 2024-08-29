using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Application.Pruebas.DesactiveLicencias;

public sealed record DesactiveLicenciasCommand(
    LicenciaId Id
): ICommand<Guid>;