using MsAcceso.Domain.Tenant.RecursosTenant;

namespace MsAcceso.Domain.Tenant.PartidasRecursosTenant;

public class PartidaRecursoTenantDto
{
    public string? Id {get; set;}
    public string? PartidaId {get; set;}
    public string? RecursoId {get; set;}
    public int? Cantidad {get; set;}
    public int? Cuadrilla {get; set;}
    public double? Precio {get; set;}
    public double? Parcial {get; set;}
    public string? TipoCuadrilla {get; private set;}

    public RecursoTenantDto? Recurso {get; set;}
}   
