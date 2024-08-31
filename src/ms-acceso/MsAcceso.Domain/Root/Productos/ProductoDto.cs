
using MsAcceso.Domain.Root.DetalleProductos;
using MsAcceso.Domain.Root.Resenias;

namespace MsAcceso.Domain.Root.Productos;
public class ProductoDto
{
    public string? Id { get; set; }
    public string? Nombre { get; set; }   
    public string? Codigo { get; set; }   
    public int? Cantidad { get; set; } 
    public DetalleProductoDto? DetalleProducto {get; private set; }
    public List<ReseniaDto>? Resenias {get; private set; }
    //public List<CategoriaDto>? Categorias {get; private set; }


}