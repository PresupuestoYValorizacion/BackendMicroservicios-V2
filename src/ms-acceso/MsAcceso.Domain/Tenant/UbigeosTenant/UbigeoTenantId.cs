namespace MsAcceso.Domain.Tenant.UbigeosTenant;
public record UbigeoTenantId(Guid Value){
    public static UbigeoTenantId New() => new(Guid.NewGuid()); 
};