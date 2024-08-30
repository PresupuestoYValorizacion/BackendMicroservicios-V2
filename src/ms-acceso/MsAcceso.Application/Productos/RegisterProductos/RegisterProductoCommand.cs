using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Productos.RegisterProductos;

public sealed record RegisterProductoCommand(
    string Nombre,
    int Cantidad
): ICommand<Guid>;