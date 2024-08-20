using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Domain.Root.Rols;

public sealed class Rol : Entity<RolId>
{
    private Rol(){}

    private Rol(
        RolId id,
        string nombre,
        SistemaId sistemaId
        ): base(id)
    {
        Nombre = nombre;
        SistemaId = sistemaId;
    }
    

    public string? Nombre {get; private set;}
    public SistemaId? SistemaId {get; private set;}


    public static Rol Create(
        string nombre,
        SistemaId sistemaId
    )
    {
        var rol = new Rol(RolId.New(), nombre, sistemaId);

        return rol;
    }


}