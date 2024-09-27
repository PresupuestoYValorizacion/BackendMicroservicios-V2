using AutoMapper;
using LinqKit;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Root.Paginations;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Root.Parametros.GetParametroByPagination;

internal sealed class GetParametroByPaginationQueryHandler : IQueryHandler<GetParametroByPaginationQuery, PagedResults<ParametroDto>?>
{
    private readonly IPaginationParametrosRepository _paginationParametrosRepository;
    private readonly IMapper _mapper;

    public GetParametroByPaginationQueryHandler(
        IPaginationParametrosRepository paginationParametrosRepository,
        IMapper mapper
    )
    {
        _paginationParametrosRepository = paginationParametrosRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResults<ParametroDto>?>> Handle(GetParametroByPaginationQuery request, CancellationToken cancellationToken)
    {
        var predicateB = PredicateBuilder.New<Parametro>(p => p.Activo == new Activo(true));


        if (!string.IsNullOrEmpty(request.Dependencia))
        {
            predicateB = predicateB.And(p => p.Dependencia == new ParametroId(int.Parse(request.Dependencia)));
        }
        else
        {
            predicateB = predicateB.And(p => p.Dependencia == null);
        }

        if (!string.IsNullOrEmpty(request.Search))
        {
            var searchPredicate = PredicateBuilder.New<Parametro>(false);
            

            searchPredicate = searchPredicate.Or(p => p.Valor!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(p => p.Nombre!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(p => p.Descripcion!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(p => p.Abreviatura!.Contains(request.Search));

            predicateB = predicateB.And(searchPredicate);
        }


        var resultPagination = await _paginationParametrosRepository.GetPaginationAsync(
                                        predicateB,
                                        null!,
                                        request.PageNumber,
                                        request.PageSize,
                                        request.OrderBy!,
                                        request.OrderAsc);

        var resultsDto = _mapper.Map<List<ParametroDto>>(resultPagination.Results);

        return new PagedResults<ParametroDto>
        {
            PageNumber = resultPagination.PageNumber,
            PageSize = resultPagination.PageSize,
            TotalNumberOfPages = resultPagination.TotalNumberOfPages,
            TotalNumberOfRecords = resultPagination.TotalNumberOfRecords,
            Results = resultsDto
        };
    }
}