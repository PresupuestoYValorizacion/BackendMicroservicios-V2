using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Tenant.ProyectosTenant;

public static class ProyectoTenantErrors
{
    public static Error NotFound = new Error(
        404,
        "No se encontro el proyecto buscado"
    );

    public static Error ProyectoExists = new(
        400,
        "El nombre de este proyecto ya existe"
    );
    
    public static Error ProyectoInUse = new(
        400,
        "El proyecto esta en uso no puede eliminarlo"
    );
}