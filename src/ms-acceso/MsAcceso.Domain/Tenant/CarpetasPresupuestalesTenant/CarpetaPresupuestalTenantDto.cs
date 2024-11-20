namespace MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;

public class CarpetaPresupuestalTenantDto
{
    public string? Id {get; set;}
    public string? Dependencia {get; set;}
    public string? Nombre {get; set;}
    public int? Nivel {get; set;}
    public bool Activo {get;set;}
    public List<CarpetaPresupuestalTenantDto>? Childrens {get; set;}
}
