using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Entity;

namespace MsAcceso.Application.Opciones.GetOpcionByPagination;

public sealed record GetAllProductsQuery : IQuery<Product?>
{
    public int Id { get; set; }
}