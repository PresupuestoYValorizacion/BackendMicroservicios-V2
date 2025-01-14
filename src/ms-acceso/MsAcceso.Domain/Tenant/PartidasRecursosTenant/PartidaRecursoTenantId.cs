namespace MsAcceso.Domain.Tenant.PartidasRecursosTenant;

public record PartidaRecursoTenantId(Guid Value)
{
    public static PartidaRecursoTenantId New() => new(Guid.NewGuid());
    
};