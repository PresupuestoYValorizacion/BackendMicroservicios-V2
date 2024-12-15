namespace MsAcceso.Domain.Tenant.UbigeosTenant;

public class UbigeoTenantDto
{
    public string? Id {get; set;}
    public string? Dependencia {get; set;}
    public string? Nombre {get; set;}
    public int? Nivel {get; set;}
    public bool Activo {get;set;}
    public List<UbigeoTenantDto>? Childrens {get; set;}
}
