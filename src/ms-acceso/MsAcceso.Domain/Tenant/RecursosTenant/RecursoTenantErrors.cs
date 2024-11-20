using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Tenant.RecursosTenant;

public static class RecursoTenantErrors
{
    public static Error NotFound = new Error(
        404,
        "No se encontro el recurso buscado"
    );

    public static Error RecursoExists = new(
        400,
        "El nombre de este recurso ya existe"
    );
    
    public static Error RecursoInUse = new(
        400,
        "El recurso esta en uso no puede eliminarlo"
    );
}