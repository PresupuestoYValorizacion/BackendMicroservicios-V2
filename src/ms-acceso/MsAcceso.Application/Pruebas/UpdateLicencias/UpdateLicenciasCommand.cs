using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Application.Pruebas.UpdateLicencias;

public sealed record UpdateLicenciasCommand(
    LicenciaId Id,
    string? Nombre
): ICommand<Guid>;