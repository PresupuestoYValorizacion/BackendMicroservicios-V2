using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.PresupuestosTenant;

namespace MsAcceso.Domain.Tenant.ProyectosTenant;

public class ProyectoTenantDto
{
    public string? Id { get; set; }
    public string? Nombre { get; set; }
    public List<EspecialidadTenantDto>?  Especialidades { get; set;}
    public PresupuestoTenantDto?  Presupuesto { get; set;}
}