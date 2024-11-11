using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Tenant.Especialidades;

public static class EspecialidadErrors
{
    public static Error NotFound = new Error(
        404,
        "No se encontro la especialidad buscada"
    );

    public static Error EspecialidadExists = new(
        400,
        "El nombre de esta especialidad ya existe"
    );
    
    public static Error EspecialidadInUse = new(
        400,
        "La especialidad esta en uso no puede eliminarlo"
    );
}