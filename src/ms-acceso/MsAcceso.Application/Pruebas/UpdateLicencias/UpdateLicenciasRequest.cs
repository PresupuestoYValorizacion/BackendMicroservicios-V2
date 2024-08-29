using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Application.Pruebas.UpdateLicencias;

public record UpdateLicenciasRequest(
    LicenciaId Id,
    string? Nombre
);