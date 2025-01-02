using MsAcceso.Domain.Tenant.EspecialidadesTenant;

namespace MsAcceso.Domain.Tenant.ProyectosTenant;

public class ProyectoTenantDto
{
    public string? Id { get; set; }
    public string? Nombre { get; set; }
    public List<EspecialidadTenantDto>?  Especialidades { get; set;}
}