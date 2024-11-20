namespace MsAcceso.Domain.Tenant.PartidasTenant;

public record PartidaTenantId(Guid Value)
{
    public static PartidaTenantId New() => new(Guid.NewGuid()); 
};