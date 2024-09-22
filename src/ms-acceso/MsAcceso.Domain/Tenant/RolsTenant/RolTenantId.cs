namespace MsAcceso.Domain.Tenant.RolsTenant;
public record RolTenantId(Guid Value){
    public static RolTenantId New() => new(Guid.NewGuid()); 
};