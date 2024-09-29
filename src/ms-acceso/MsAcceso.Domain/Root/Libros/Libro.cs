using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.Libros;

public sealed class Libro : Entity<LibroId>
{
    private Libro() { }

    private Libro(
        LibroId id,
        string nombre,
        string descipcion,
        double precio
    ) : base(id)
    {
        Nombre = nombre;
        Descripcion = descipcion;
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
        var opcion = new Libro(LibroId.New(), Nombre, Descripcion, Precio);
        return opcion;
    }

    public Result Update(
        string nombre,
        string descipcion,
        double precio
    )
    {
        Nombre = (nombre.Length > 0) ? nombre : Nombre;
        Descripcion = (descipcion.Length > 0) ? descipcion : Descripcion;
        Precio = (precio > 0) ? precio : Precio;

        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}