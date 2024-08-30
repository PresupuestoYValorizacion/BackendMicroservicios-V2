using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Productos;

namespace MsAcceso.Application.Productos.UpdateProductos;

public sealed record UpdateProductoCommand(
    ProductoId Id,
    string Nombre, 
    string Codigo, 
    int Cantidad
): ICommand<Guid>;