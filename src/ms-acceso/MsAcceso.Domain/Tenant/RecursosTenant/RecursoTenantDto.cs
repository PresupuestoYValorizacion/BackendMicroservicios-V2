using MsAcceso.Domain.Tenant.PartidasRecursosTenant;
using MsAcceso.Domain.Tenant.PartidasTenant;

namespace MsAcceso.Domain.Tenant.RecursosTenant;

public class RecursoTenantDto
{
    public string? Id { get; set; }
    public string? Nombre { get; set; }
    public int? TipoRecursoId { get; set; }
    public int UnidadMedidaId { get; set; }
    public List<PartidaTenantDto>? Partidas {get; private set;}
    public List<PartidaRecursoTenantDto>? PartidasRecursos {get; private set;}

}