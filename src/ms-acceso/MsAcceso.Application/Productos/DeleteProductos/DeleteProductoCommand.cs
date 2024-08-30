using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Productos;

namespace MsAcceso.Application.Productos.DeleteProductos;

public sealed record DeleteProductoCommand(
    ProductoId Id
): ICommand<Guid>;