namespace MsAcceso.Domain.Tenant.RolPermisosOpcionesTenant;

public record RolPermisoOpcionTenantId(Guid Value){
    public static RolPermisoOpcionTenantId New() => new(Guid.NewGuid()); 
};