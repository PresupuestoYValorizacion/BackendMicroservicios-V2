using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Opciones.GetOpcionByPagination;

public sealed record GetOpcionByPaginationQuery : PaginationParams, IQuery<PagedResults<OpcionDto>?>
{

}