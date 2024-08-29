

using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Root.Licencias;


public static class LicenciaErrors
{

    public static Error NotFound = new(
        404,
        "No existe la licencia buscada"
    );
    public static Error LicenciaExists = new(
        400,
        "Esta licencia ya existe, cree una nueva"
    );

}