using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Categorias;
using MsAcceso.Domain.Root.DetalleProductos;
using MsAcceso.Domain.Root.ProductoProductoCategorias;
using MsAcceso.Domain.Root.Resenias;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.Productos;

public sealed class Producto : Entity<ProductoId>
{
    private Producto(){}

    private Producto(
        ProductoId id,
        string nombre,
        string codigo,
        int cantidad
    ): base(id)
    {
        Nombre = nombre;
        Codigo = codigo;
        Cantidad = cantidad;
    }
    
    public string? Nombre {get; private set; }
    public string? Codigo {get; private set; }
    public int Cantidad {get; private set; }
    public DetalleProducto? DetalleProducto {get; private set; }
    public List<Resenia> Resenias { get; } = new List<Resenia>();
    public List<Categoria> Categorias { get; } = [];
    public List<ProductoCategoria> ProductoCategorias { get; } = [];
    public static Producto Create(
        string nombre,
        string codigo,
        int cantidad
    )
    {
        var producto = new Producto(ProductoId.New(), nombre, codigo, cantidad);

        return producto;
    }

    public Result Update(
        string nombre,
        string codigo,
        int cantidad
    )
    {
        Nombre = (nombre.Length > 0) ? nombre : Nombre;
        Codigo   = (codigo.Length > 0) ? codigo : Codigo;
        Cantidad = (cantidad > 0) ? cantidad : Cantidad;

        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}