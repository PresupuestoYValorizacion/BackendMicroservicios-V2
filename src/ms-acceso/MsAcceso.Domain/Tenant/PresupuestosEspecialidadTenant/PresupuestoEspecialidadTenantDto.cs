using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.ProyectosTenant;

namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTenant;

public class PresupuestoEspecialidadTenantDto
{
    public string? Id { get; set; }
    public string? EspecialidadId { get; set; }
    public EspecialidadTenantDto? Especialidad{get; set;} 
    public string? ProyectoId { get; set; }
    public ProyectoTenantDto? Proyecto {get; set;} 
}