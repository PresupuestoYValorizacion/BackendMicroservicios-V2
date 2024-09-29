using AutoMapper;
using LinqKit;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Root.Paginations;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Libros;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Root.Libros.GetLibroByPagination;

internal sealed class GetLibroByPaginationQueryHandler : IQueryHandler<GetLibroByPaginationQuery, PagedResults<LibroDto>?>
{
    private readonly IPaginationLibroRepository _paginationLibroRepository;
    private readonly IMapper _mapper;

    public GetLibroByPaginationQueryHandler(
        IPaginationLibroRepository paginationLibroRepository,
        IMapper mapper
    )
    {
        _paginationLibroRepository = paginationLibroRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResults<LibroDto>?>> Handle(GetLibroByPaginationQuery request, CancellationToken cancellationToken)
    {
        var predicateB = PredicateBuilder.New<Libro>(p => p.Activo == new Activo(true));


        if (!string.IsNullOrEmpty(request.Search))
        {
            var searchPredicate = PredicateBuilder.New<Libro>(false);

            searchPredicate = searchPredicate.Or(o => o.Nombre!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(o => o.Descripcion!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(o => o.Precio!.Value.Equals(request.Search));

            predicateB = predicateB.And(searchPredicate);
        }

        var resultPagination = await _paginationLibroRepository.GetPaginationAsync(
                                            predicateB,
                                            null!,
                                            request.PageNumber,
                                            request.PageSize,
                                            request.OrderBy!,
                                            request.OrderAsc
                                        );

        var resultsDto = _mapper.Map<List<LibroDto>>(resultPagination.Results);

        return new PagedResults<LibroDto>
        {
            PageNumber = resultPagination.PageNumber,
            PageSize = resultPagination.PageSize,
            TotalNumberOfPages = resultPagination.TotalNumberOfPages,
            TotalNumberOfRecords = resultPagination.TotalNumberOfRecords,
            Results = resultsDto
        };
    }
}