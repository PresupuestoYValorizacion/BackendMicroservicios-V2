using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Tenant.ClientesTenant;

public static class ClienteTenantErrors
{
    public static Error ClienteNotFound = new(400, "Este cliente no existe");
    // public static Error CarpetaPresupuestalIntercambioNotFound = new(400, "La carpeta presupuestal para intercambiar no existe");
    // public static Error CarpetaPresupuestalNotAvailable = new(400, "Esta carpeta presupuestal no existe, o esta desactivada");
    public static Error ClienteExists = new(400, "Este cliente ya existe, ingrese otro.");
    public static Error ClienteInUse = new(400, "El cliente esta en uso no puede eliminarla.");
}