using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Productos;

namespace MsAcceso.Application.Productos.DesactiveProductos;

public sealed record DesactiveProductoCommand(
    ProductoId Id
): ICommand<Guid>;