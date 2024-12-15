namespace MsAcceso.Domain.Tenant.EspecialidadesTenant;

public record EspecialidadTenantId(Guid Value)
{
    public static EspecialidadTenantId New() => new(Guid.NewGuid());
    
};