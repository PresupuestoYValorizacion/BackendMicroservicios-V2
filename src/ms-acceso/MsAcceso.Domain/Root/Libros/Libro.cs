using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.Libros;

public sealed class Libro : Entity<LibroId>
{
    private Libro(){}

    private Libro(
        LibroId id,
        string nombre,
        string descripcion,
        double precio
    ) : base(id)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        Precio = precio;

    }

    public string? Nombre { get; private set; }
    public string? Descripcion { get; private set; }
    public double? Precio { get; private set; }
    

    public static Libro Create(
        string Nombre,
        string Descripcion,
        double Precio
    )
    {
        var Libro = new Libro(LibroId.New(),Nombre,Descripcion,Precio);
        return Libro;
    }

    public Result Update(
        string nombre,
        string descripcion,
        double precio
    )
    {
        Nombre = (nombre.Length > 0) ? nombre : Nombre;
        Descripcion   = descripcion;
        Precio =  precio;

        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}