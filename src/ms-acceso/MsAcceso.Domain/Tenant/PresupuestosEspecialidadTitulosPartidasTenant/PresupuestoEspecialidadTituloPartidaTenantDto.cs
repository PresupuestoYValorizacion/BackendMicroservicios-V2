
using MsAcceso.Domain.Tenant.PartidasTenant;

namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant;

public class PresupuestoEspecialidadTituloPartidaTenantDto
{
    public string? Id {get;  set;}
    public string? Correlativo {get;  set;}
    public PartidaTenantDto? Partida {get; set;}
}