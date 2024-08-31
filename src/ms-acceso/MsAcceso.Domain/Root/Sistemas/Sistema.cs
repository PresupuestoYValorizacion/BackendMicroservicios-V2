using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.Sistemas;

public sealed class Sistema : Entity<SistemaId>
{
    private Sistema(){}

    private Sistema(
        SistemaId id,
        SistemaId? dependencia,
        string nombre,
        string logo,
        int nivel,
        string url,
        ParametroId? tipo
    ): base(id)
    {
        Dependencia = dependencia;
        Nombre = nombre;
        Logo = logo;
        Nivel = nivel;
        Url = url;
    }

    public SistemaId? Dependencia {get; private set;}
    public Sistema? DependenciaModel {get; private set;}
    public string? Nombre {get; private set;}
    public string? Logo {get; private set;}
    public int Nivel {get; private set;}
    public string? Url {get; private set;}
    public List<Sistema>? Sistemas { get; set; }
    public List<Opcion>? Opciones { get; set; } 
    public List<MenuOpcion>? MenuOpcions { get; set; } 


    public static Sistema Create(SistemaId? dependencia, string nombre, string logo, int nivel, string url)
    {
        var sistema = new Sistema(SistemaId.New(),dependencia,nombre,logo,nivel,url, null);
        return sistema;
    }

    public Result Update(string nombre, string logo,string url)
    {
        Nombre = (nombre.Length > 0 ) ? nombre : Nombre;
        Logo = (logo.Length > 0 ) ? logo : Logo;
        Url = (url.Length > 0 ) ? url : Url;

        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}
