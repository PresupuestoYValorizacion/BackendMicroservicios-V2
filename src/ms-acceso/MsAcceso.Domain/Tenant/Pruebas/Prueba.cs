
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;


namespace MsAcceso.Domain.Tenant.Pruebas;

public sealed class Prueba : Entity<PruebaId>
{
    private Prueba() {}

    private Prueba(
        PruebaId id,
        string nombre
        ): base(id)
    {
        Nombre = nombre;
    }

    public string? Nombre {get; private set;} 

    public static Prueba Create(
        string nombre
    )
    {
        var prueba = new Prueba(PruebaId.New(), nombre);

        return prueba;
    }

    public Result Update(string nombre)
    {
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }


}
