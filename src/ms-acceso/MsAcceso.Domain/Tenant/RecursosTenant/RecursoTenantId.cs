namespace MsAcceso.Domain.Tenant.RecursosTenant;

public record RecursoTenantId(Guid Value)
{
    public static RecursoTenantId New() => new(Guid.NewGuid());
    
};