using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Domain.Root.Sistemas;

public class SistemaDto
{
    public string? Id {get; set;}
    public string? Dependencia {get; set;}
    public string? Nombre {get; set;}
    public string? Logo {get; set;}
    public int? Nivel {get; set;}
    public string? Url {get; set;}
    public List<SistemaDto>? Childrens {get; set;}
    public List<OpcionDto>? Opciones { get; set; } 

}