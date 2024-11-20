using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;

public static class CarpetaPresupuestalTenantErrors
{
    public static Error CarpetaPresupuestalNotFound = new(400, "Esta carpeta presupuestal no existe");
    public static Error CarpetaPresupuestalIntercambioNotFound = new(400, "La carpeta presupuestal para intercambiar no existe");
    public static Error CarpetaPresupuestalNotAvailable = new(400, "Esta carpeta presupuestal no existe, o esta desactivada");
    public static Error CarpetaPresupuestalNameExists = new(400, "Este nombre de carpeta presupuestal ya existe, ingrese otro.");
    public static Error CarpetaPresupuestalInUse = new(400, "La carpeta presupuestal esta en uso no puede eliminarla.");
}