namespace MsAcceso.Domain.Tenant.UsersTenant;

public record UserTenantId(Guid Value){
    public static UserTenantId New() => new(Guid.NewGuid()); 
};