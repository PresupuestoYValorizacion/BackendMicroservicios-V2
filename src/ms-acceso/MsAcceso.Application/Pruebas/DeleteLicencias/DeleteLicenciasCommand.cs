using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Application.Pruebas.DeleteLicencias;

public sealed record DeleteLicenciasCommand(
    LicenciaId Id
): ICommand<Guid>;