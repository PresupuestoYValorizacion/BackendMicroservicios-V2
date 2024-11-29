namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant;

public record PresupuestoEspecialidadTituloPartidaTenantId(Guid Value)
{
    public static PresupuestoEspecialidadTituloPartidaTenantId New() => new(Guid.NewGuid()); 
};