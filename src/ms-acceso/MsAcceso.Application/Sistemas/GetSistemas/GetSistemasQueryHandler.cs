using AutoMapper;
using LinqKit;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Paginations;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Sistemas.GetSistemas;

internal sealed class GetSistemasQueryHandler : IQueryHandler<GetSistemasQuery, List<SistemasDto>>
{
    // private readonly IPaginationSistemaRepository _paginationSistemaRepository;
    private readonly ISistemaRepository _sistemaRepository;
    private readonly IMapper _mapper;

    public GetSistemasQueryHandler(
        // IPaginationSistemaRepository paginationSistemaRepository,
        ISistemaRepository sistemaRepository,
        IMapper mapper
    )
    {
        // _paginationSistemaRepository = paginationSistemaRepository;
        _sistemaRepository = sistemaRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<SistemasDto>>> Handle(GetSistemasQuery request, CancellationToken cancellationToken)
    {
        var sistemas = await _sistemaRepository.GetAllSistemas(cancellationToken);

        var sistemasDto = _mapper.Map<List<SistemasDto>>(sistemas);

        return sistemasDto!;
    }

    // public async Task<Result<PagedResults<SistemasDto>>> Handle(GetSistemasQuery request, CancellationToken cancellationToken)
    // {
    //     var predicateB = PredicateBuilder.New<Sistema>(s => s.Activo == new Activo(true));

    //     if(!string.IsNullOrEmpty(request.Dependencia))
    //     {
    //         predicateB = predicateB.And(s => s.Dependencia == new SistemaId(Guid.Parse(request.Dependencia)));
    //     }
    //     else
    //     {
    //         predicateB = predicateB.And(s => s.Dependencia == null);
    //     }

    //     if(!string.IsNullOrEmpty(request.Search))
    //     {
    //         var searchPredicate = PredicateBuilder.New<Sistema>(false);

    //         searchPredicate = searchPredicate.Or(s => s.Nombre!.Contains(request.Search));
    //         searchPredicate = searchPredicate.Or(s => s.Logo!.Contains(request.Search));
    //         searchPredicate = searchPredicate.Or(s => s.Url!.Contains(request.Search));
    //     }

    //     var resultPagination = await _paginationSistemaRepository.GetPaginationAsync(
    //         predicateB,
    //         null!,
    //         request.PageNumber,
    //         request.PageSize,
    //         request.OrderBy!,
    //         request.OrderAsc
    //     );

    //     var resultsDto = _mapper.Map<List<SistemasDto>>(resultPagination.Results);

    //     return new PagedResults<SistemasDto>
    //     {
    //         PageNumber = resultPagination.PageNumber,
    //         PageSize = resultPagination.PageSize,
    //         TotalNumberOfPages = resultPagination.TotalNumberOfPages,
    //         TotalNumberOfRecords = resultPagination.TotalNumberOfRecords,
    //         Results = resultsDto
    //     };
    // }
}