using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Tenant.TitulosTenant;

public static class TituloTenantErrors
{
    public static Error NotFound = new Error(
        404,
        "No se encontro el titulo buscado"
    );

    public static Error TituloExists = new(
        400,
        "El nombre de este titulo ya existe"
    );
    
    public static Error TituloInUse = new(
        400,
        "El titulo esta en uso no puede eliminarlo"
    );
}