using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Domain.Root.Rols;

public sealed class Rol : Entity<RolId>
{
    private Rol(){}

    private Rol(
        RolId id,
        string nombre
        ): base(id)
    {
        Nombre = nombre;
    }
    

    public string? Nombre {get; private set;}


    public static Rol Create(
        string nombre
    )
    {
        var rol = new Rol(RolId.New(), nombre);

        return rol;
    }


}