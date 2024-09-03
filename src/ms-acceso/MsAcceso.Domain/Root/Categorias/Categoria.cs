using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Productos;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.Categorias;

public sealed class Categoria : Entity<CategoriaId>
{
    private Categoria(){}

    private Categoria(
        CategoriaId id,
        string nombre
    ): base(id)
    {
        Nombre = nombre;
    }
    
    public string? Nombre {get; private set; }
    //public List<Producto> Productos { get; } = [];

    public static Categoria Create(
        CategoriaId id,
        string nombre
    )
    {
        var detalleResenia = new Categoria(id, nombre);

        return detalleResenia;
    }

    public Result Update(
        string nombre
    )
    {
        Nombre = (nombre.Length > 0) ? nombre : Nombre;

        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}