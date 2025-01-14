using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Tenant.PartidasRecursosTenant;

public static class PartidaRecursoTenantErrors
{
    public static Error NotFound = new Error(
        404,
        "No se encontro la partida recurso buscada"
    );

    public static Error EspecialidadExists = new(
        400,
        "El nombre de esta partida recurso ya existe"
    );

    public static Error EspecialidadNotExists = new(
        400,
        "Este partida recurso no existe"
    );
    
    public static Error EspecialidadInUse = new(
        400,
        "El partida recurso esta en uso no puede eliminarlo"
    );
}