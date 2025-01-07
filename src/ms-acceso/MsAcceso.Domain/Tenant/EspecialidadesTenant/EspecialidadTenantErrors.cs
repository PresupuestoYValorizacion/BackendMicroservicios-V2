using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Tenant.EspecialidadesTenant;

public static class EspecialidadTenantErrors
{
    public static Error NotFound = new Error(
        404,
        "No se encontro la especialidad buscada"
    );

    public static Error EspecialidadExists = new(
        400,
        "El nombre de esta especialidad ya existe"
    );

    public static Error EspecialidadNotExists = new(
        400,
        "Esta especialidad no existe"
    );
    
    public static Error EspecialidadInUse = new(
        400,
        "La especialidad esta en uso no puede eliminarlo"
    );
}