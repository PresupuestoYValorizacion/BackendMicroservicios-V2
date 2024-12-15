namespace MsAcceso.Domain.Tenant.TitulosTenant;

public record TituloTenantId(Guid Value)
{
    public static TituloTenantId New() => new(Guid.NewGuid());
    
};