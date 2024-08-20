using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Parametros.GetParametroByPagination;

public sealed record GetParametroByPaginationQuery : PaginationParams, IQuery<PagedResults<ParametroDto>?>
{
    public string? Dependencia {get;set;}
}