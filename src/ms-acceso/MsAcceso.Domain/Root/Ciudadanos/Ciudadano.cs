using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Pasaportes;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.Ciudadanos;

public sealed class Ciudadano : Entity<CiudadanoId>
{
    private Ciudadano() {}

    private Ciudadano(
        CiudadanoId id,
        string apellido,
        string nacionalidad
    ): base(id)
    {
        Apellido = apellido;
        Nacionalidad = nacionalidad;
    }

    public string? Apellido {get; private set;}
    public string? Nacionalidad {get; private set;}
    public Pasaporte? Pasaporte {get; private set;}

    public static Ciudadano Create(
        string Apellido,
        string Nacionalidad
    )
    {
        var ciudadano = new Ciudadano(CiudadanoId.New(), Apellido, Nacionalidad);
        return ciudadano;
    }
    public Result Update(
        string apellido,
        string nacionalidad
    )
    {
        Apellido = (apellido.Length > 0) ? apellido : Apellido;
        Nacionalidad = (nacionalidad.Length > 0) ? nacionalidad : Nacionalidad;

        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}