
using MsAcceso.Domain.Tenant.TitulosTenant;

namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;

public class PresupuestoEspecialidadTituloTenantDto
{
    public string? Id {get;  set;}
    public string? Correlativo {get;  set;}
    public TituloTenantDto? Titulo {get; set;}
}