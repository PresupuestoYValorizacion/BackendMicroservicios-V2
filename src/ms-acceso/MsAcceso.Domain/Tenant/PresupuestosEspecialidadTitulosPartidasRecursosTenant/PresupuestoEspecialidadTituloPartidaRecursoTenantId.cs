namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasRecursosTenant;

public record PresupuestoEspecialidadTituloPartidaRecursoTenantId(Guid Value)
{
    public static PresupuestoEspecialidadTituloPartidaRecursoTenantId New() => new(Guid.NewGuid()); 
};