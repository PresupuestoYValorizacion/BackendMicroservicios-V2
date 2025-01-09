using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;

public static class PresupuestoEspecialidadTituloTenantErrors
{
    public static Error NotFound = new Error(
        404,
        "No se encontro el presupuesto buscado"
    );

    public static Error PresupuestoExists = new(
        400,
        "El nombre de este titulo ya existe"
    );

     public static Error PresupuestoNotExists = new(
        404,
        "El presupuesto no existe"
    );
    
    public static Error PresupuestoInUse = new(
        400,
        "El presupuesto esta en uso no puede eliminarlo"
    );
}