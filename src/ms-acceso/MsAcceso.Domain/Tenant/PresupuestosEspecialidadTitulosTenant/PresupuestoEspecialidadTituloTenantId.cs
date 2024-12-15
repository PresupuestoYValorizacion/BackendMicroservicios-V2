namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;

public record PresupuestoEspecialidadTituloTenantId(Guid Value)
{
    public static PresupuestoEspecialidadTituloTenantId New() => new(Guid.NewGuid()); 
};