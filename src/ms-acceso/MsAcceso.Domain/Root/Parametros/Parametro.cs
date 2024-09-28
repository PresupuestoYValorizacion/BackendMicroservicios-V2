
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.Parametros;

public sealed class Parametro : Entity<ParametroId>
{
    private Parametro(){}

    private Parametro(
        ParametroId id,
        string nombre,
        string? abreviatura,
        string? descripcion,
        ParametroId? dependencia,
        int nivel,
        string? valor
        ) : base(id)
    {
        Nombre = nombre;
        Abreviatura = abreviatura;
        Descripcion = descripcion;
        Dependencia = dependencia;
        Nivel = nivel;
        Valor = valor;
    }
    public string? Nombre { get; private set; }
    public string? Abreviatura{ get; private set; }
    public string? Descripcion{ get; private set; }
    public ParametroId? Dependencia { get; private set; }
    public string? Valor {get; private set;}
    public int? Nivel { get; private set; }
 
    public static Parametro Create(
        ParametroId Id,
        string nombre,
        string? abreviatura,
        string? descripcion,
        ParametroId? dependencia,
        int nivel,
        string? valor
    )
    {
        var parametro = new Parametro(Id, nombre, abreviatura, descripcion, dependencia, nivel, valor);

        return parametro;
    }

    public Result Update(string nombre, string abreviatura, string descripcion, string valor)
    {
        Nombre = nombre.Length > 0 ? nombre : Nombre;
        Abreviatura = abreviatura ;
        Descripcion = descripcion;
        Valor = valor;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}