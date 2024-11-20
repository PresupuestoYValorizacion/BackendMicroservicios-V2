using MsAcceso.Domain.Tenant.PartidasRecursosTenant;
using MsAcceso.Domain.Tenant.RecursosTenant;

namespace MsAcceso.Domain.Tenant.PartidasTenant;

public class PartidaTenantDto
{
    public string? Id {get; set;}
    public string? Dependencia {get; set;}
    public string? Nombre {get; set;}
    public int? Nivel {get; set;}
    public bool Activo {get;set;}
    public List<PartidaTenantDto>? Childrens {get; set;}
    public List<RecursoTenantDto>? Recursos {get; private set;}
    public List<PartidaRecursoTenantDto>? PartidasRecursos {get; private set;}
}
