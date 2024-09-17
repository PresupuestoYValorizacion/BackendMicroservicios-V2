using AutoMapper;
using LinqKit;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Paginations;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Opciones.GetOpcionByPagination;

internal sealed class GetOpcionByPaginationQueryHandler : IQueryHandler<GetOpcionByPaginationQuery, PagedResults<OpcionDto>?>
{
    private readonly IPaginationOpcionRepository _paginationOpcionRepository;
    private readonly IMapper _mapper;

    public GetOpcionByPaginationQueryHandler(
        IPaginationOpcionRepository paginationOpcionRepository,
        IMapper mapper
    )
    {
        _paginationOpcionRepository = paginationOpcionRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResults<OpcionDto>?>> Handle(GetOpcionByPaginationQuery request, CancellationToken cancellationToken)
    {   
        var predicateB = PredicateBuilder.New<Opcion>(p => p.Activo == new Activo(true));
        

        if(!string.IsNullOrEmpty(request.Search))
        {
            var searchPredicate = PredicateBuilder.New<Opcion>(false);

            searchPredicate = searchPredicate.Or(o => o.Nombre!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(o => o.Icono!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(o => o.Tooltip!.Contains(request.Search));

            predicateB = predicateB.And(searchPredicate);
        }

        var resultPagination = await _paginationOpcionRepository.GetPaginationAsync(
                                            predicateB,
                                            null!,
                                            request.PageNumber,
                                            request.PageSize,
                                            request.OrderBy!,
                                            request.OrderAsc
                                        );

        var resultsDto = _mapper.Map<List<OpcionDto>>(resultPagination.Results);

        return new PagedResults<OpcionDto>
        {
            PageNumber = resultPagination.PageNumber,
            PageSize = resultPagination.PageSize,
            TotalNumberOfPages = resultPagination.TotalNumberOfPages,
            TotalNumberOfRecords = resultPagination.TotalNumberOfRecords,
            Results = resultsDto
        };
    }
}