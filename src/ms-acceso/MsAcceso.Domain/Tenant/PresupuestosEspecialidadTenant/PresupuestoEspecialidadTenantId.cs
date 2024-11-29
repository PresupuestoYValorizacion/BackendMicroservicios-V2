namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTenant;

public record PresupuestoEspecialidadTenantId(Guid Value)
{
    public static PresupuestoEspecialidadTenantId New() => new(Guid.NewGuid()); 
};