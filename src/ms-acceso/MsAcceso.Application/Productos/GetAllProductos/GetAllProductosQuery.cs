using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Productos;

namespace MsAcceso.Application.Productos.GetAllProductos;

public sealed record GetAllProductosQuery : IQuery<List<ProductoDto>>
{

}