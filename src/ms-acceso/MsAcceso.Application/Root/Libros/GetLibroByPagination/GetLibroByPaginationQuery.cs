using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Libros;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Root.Libros.GetLibroByPagination;

public sealed record GetLibroByPaginationQuery : PaginationParams, IQuery<PagedResults<LibroDto>?>
{

}