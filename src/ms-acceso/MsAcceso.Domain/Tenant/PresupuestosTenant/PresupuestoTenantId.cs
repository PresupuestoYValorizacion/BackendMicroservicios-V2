namespace MsAcceso.Domain.Tenant.PresupuestosTenant;

public record PresupuestoTenantId(Guid Value)
{
    public static PresupuestoTenantId New() => new(Guid.NewGuid()); 
};