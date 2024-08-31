using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.Opciones;

public sealed class Opcion : Entity<OpcionId>
{
    private Opcion(){}

    private Opcion(
        OpcionId id,
        string nombre,
        string logo,
        string abreviatura
    ) : base(id)
    {
        Nombre = nombre;
        Logo = logo;
        Abreviatura = abreviatura;
    }

    public string? Nombre { get; private set; }
    public string? Logo { get; private set; }
    public string? Abreviatura { get; private set; }

    public static Opcion Create(
        string Nombre,
        string Logo,
        string Abreviatura
    )
    {
        var opcion = new Opcion(OpcionId.New(),Nombre,Logo,Abreviatura);
        return opcion;
    }

    public Result Update(
        string nombre,
        string logo,
        string abreviatura
    )
    {
        Nombre = (nombre.Length > 0) ? nombre : Nombre;
        Logo   = (logo.Length > 0) ? logo : Logo;
        Abreviatura = (abreviatura.Length > 0) ? abreviatura : Abreviatura;

        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}