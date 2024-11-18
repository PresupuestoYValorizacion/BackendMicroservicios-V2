using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Tenant.UbigeosTenant;

public static class UbigeoTenantErrors
{
    public static Error UbigeoNotFound = new(400, "Este ubigeo no existe");
    public static Error UbigeoIntercambioNotFound = new(400, "El ubigeo para intercambiar no existe");
    public static Error UbigeoNotAvailable = new(400, "Este ubigeo no existe, o esta desactivado");
    public static Error UbigeoNameExists = new(400, "Este nombre de ubigeo ya existe, ingrese otro.");
    public static Error UbigeoUrlExists = new(400, "Este url de ubigeo ya existe, ingrese otro.");
    public static Error UbigeoInUse = new(400, "El menu esta en uso no puede eliminarlo.");
}