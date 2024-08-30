namespace MsAcceso.Application.Productos.UpdateProductos;

public record UpdateProductoRequest(
    Guid Id,
    string Nombre,
    string Codigo,
    int Cantidad
);