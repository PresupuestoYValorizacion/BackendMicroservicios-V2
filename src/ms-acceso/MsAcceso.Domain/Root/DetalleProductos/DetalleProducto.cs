using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Productos;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.DetalleProductos;

public sealed class DetalleProducto : Entity<ProductoId>
{
    private DetalleProducto(){}

    private DetalleProducto(
        ProductoId id,
        string descripcion,
        DateTime fechaCreacion
    ): base(id)
    {
        Descripcion = descripcion;
        FechaCreacion = fechaCreacion;
    }
    
    public string? Descripcion {get; private set; }
    public DateTime? FechaCreacion {get; private set; }

    public static DetalleProducto Create(
        ProductoId id,
        string descripcion,
        DateTime fechaCreacion
    )
    {
        var detalleProducto = new DetalleProducto(id, descripcion, fechaCreacion);

        return detalleProducto;
    }

    public Result Update(
        string descripcion,
        DateTime fechaCreacion
    )
    {
        Descripcion = (descripcion.Length > 0) ? descripcion : Descripcion;
        FechaCreacion   = fechaCreacion;

        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}