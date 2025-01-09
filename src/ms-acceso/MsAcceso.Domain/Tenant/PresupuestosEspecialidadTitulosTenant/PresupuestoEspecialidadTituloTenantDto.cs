using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.ClientesTenant;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.TitulosTenant;

namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;

public class PresupuestoEspecialidadTituloTenantDto
{
    public string? Id {get;  set;}
    public string? Correlativo {get;  set;}
    public TituloTenantDto? Titulo {get; set;}
}