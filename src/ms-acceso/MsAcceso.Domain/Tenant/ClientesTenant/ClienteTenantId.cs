
namespace MsAcceso.Domain.Tenant.ClientesTenant;
public record ClienteTenantId(Guid Value){
    public static ClienteTenantId New() => new(Guid.NewGuid()); 
};