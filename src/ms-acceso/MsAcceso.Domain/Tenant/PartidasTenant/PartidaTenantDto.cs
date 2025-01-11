

namespace MsAcceso.Domain.Tenant.PartidasTenant;

public class PartidaTenantDto
{
    public string? Id {get; set;}
    public string? Nombre {get; set;}
    public string? Correlativo {get; set;}
    public List<PartidaTenantDto>? Partidas {get; set;}
}
