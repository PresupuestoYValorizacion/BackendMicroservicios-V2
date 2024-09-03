using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Categorias;
using MsAcceso.Domain.Root.ProductoCategorias;
using MsAcceso.Domain.Root.Productos;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.ProductoProductoCategorias;

public sealed class ProductoCategoria : Entity<ProductoCategoriaId>
{
    private ProductoCategoria(){}

    private ProductoCategoria(
        ProductoCategoriaId id,
        ProductoId productoId,
        CategoriaId categoriaId
    ): base(id)
    {
        ProductoId = productoId;
        CategoriaId = categoriaId;
    }
    
    public ProductoId? ProductoId {get; private set; }
    public CategoriaId? CategoriaId {get; private set; }
    public Producto? Producto {get; private set; }
    public Categoria? Categoria {get; private set; }

    public static ProductoCategoria Create(
        ProductoCategoriaId id,
        ProductoId productoId,
        CategoriaId categoriaId
    )
    {
        var detalleResenia = new ProductoCategoria(id, productoId, categoriaId);

        return detalleResenia;
    }

    public Result Update(
        ProductoId productoId,
        CategoriaId categoriaId
    )
    {
        ProductoId = productoId;
        CategoriaId = categoriaId;

        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}