using AutoMapper;
using LinqKit;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Sgo.Paginations;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Application.Sgo.Clientes.GetClienteByPagination;

internal sealed class GetClienteByPaginationQueryHandler : IQueryHandler<GetClienteByPaginationQuery, PagedResults<ClienteDto>?>
{
    private readonly IPaginationClientesRepository _paginationClientesRepository;
    private readonly IMapper _mapper;

    public GetClienteByPaginationQueryHandler(
        IPaginationClientesRepository paginationClientesRepository,
        IMapper mapper
    )
    {
        _paginationClientesRepository = paginationClientesRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResults<ClienteDto>?>> Handle( GetClienteByPaginationQuery request, CancellationToken cancellationToken)
    {
        var predicateB = PredicateBuilder.New<ClienteTenant>(p => p.Activo == new Activo(true));


        if (!string.IsNullOrEmpty(request.Search))
        {
            var searchPredicate = PredicateBuilder.New<ClienteTenant>(false);
            

            searchPredicate = searchPredicate.Or(p => p.NumeroDocumento!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(p => p.Nombre!.Contains(request.Search));
            

            predicateB = predicateB.And(searchPredicate);
        }


        var resultPagination = await _paginationClientesRepository.GetPaginationAsync(
                                        predicateB,
                                        null!,
                                        request.PageNumber,
                                        request.PageSize,
                                        request.OrderBy!,
                                        request.OrderAsc);

        var resultsDto = _mapper.Map<List<ClienteDto>>(resultPagination.Results);

        return new PagedResults<ClienteDto>
        {
            PageNumber = resultPagination.PageNumber,
            PageSize = resultPagination.PageSize,
            TotalNumberOfPages = resultPagination.TotalNumberOfPages,
            TotalNumberOfRecords = resultPagination.TotalNumberOfRecords,
            Results = resultsDto
        };
    }
}