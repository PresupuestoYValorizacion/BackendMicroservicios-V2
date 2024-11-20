namespace MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;

public record CarpetaPresupuestalTenantId(Guid Value)
{
    public static CarpetaPresupuestalTenantId New() => new(Guid.NewGuid()); 
};