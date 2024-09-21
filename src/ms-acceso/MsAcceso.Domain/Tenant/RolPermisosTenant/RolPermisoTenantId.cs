namespace MsAcceso.Domain.Tenant.RolPermisosTenant;

public record RolPermisoTenantId(Guid Value){
    public static RolPermisoTenantId New() => new(Guid.NewGuid()); 
};