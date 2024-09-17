using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.RolPermisos;

namespace MsAcceso.Domain.Root.Sistemas;

public class SistemaDto
{
    public string? Id {get; set;}
    public string? Dependencia {get; set;}
    public string? Nombre {get; set;}
    public string? Logo {get; set;}
    public int? Nivel {get; set;}
    public int? Orden {get; set;}
    public string? Url {get; set;}
    public bool Activo {get;set;}
    public List<SistemaDto>? Childrens {get; set;}
    public List<OpcionDto>? Opciones { get; set; } 
    public List<MenuOpcionDto>? MenuOpciones { get; set; } 

}


public class SistemaByRolDto
{
    public string? Id {get; set;}
    public string? Nombre {get; set;}
    public string? Dependencia {get; set;}

    public string? Logo {get; set;}
    public int? Nivel {get; set;}
    public string? Url {get; set;}
    public List<SistemaByRolDto>? Childrens {get; set;}
    public List<MenuOpcionDto>? MenuOpciones { get; set; } 
    public bool Completed {get; set;}

}