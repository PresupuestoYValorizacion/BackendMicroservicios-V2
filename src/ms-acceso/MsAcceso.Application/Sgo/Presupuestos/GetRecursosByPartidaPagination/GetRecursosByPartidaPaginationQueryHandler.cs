using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Sgo.Paginations;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PartidasRecursosTenant;
using MsAcceso.Domain.Tenant.PartidasTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.GetRecursosByPartidaPagination;

internal sealed class GetRecursosByPartidaPaginationQueryHandler : IQueryHandler<GetRecursosByPartidaPaginationQuery, PagedResults<PartidaRecursoTenantDto>?>
{
    private readonly IPaginationRecursosRepository _paginationRecursosRepository;
    private readonly IParametroRepository _parametroRepository;
    private readonly IPartidaTenantRepository _partidaRepository;
    private readonly IMapper _mapper;

    public GetRecursosByPartidaPaginationQueryHandler(
        IPaginationRecursosRepository paginationRecursosRepository,
        IParametroRepository parametroRepository,
        IPartidaTenantRepository partidaRepository,
        IMapper mapper
    )
    {
        _paginationRecursosRepository = paginationRecursosRepository;
        _partidaRepository = partidaRepository;
        _parametroRepository = parametroRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResults<PartidaRecursoTenantDto>?>> Handle( GetRecursosByPartidaPaginationQuery request, CancellationToken cancellationToken)
    {
        var partidaId = new PartidaTenantId(Guid.Parse(request.PartidaId!));
        var partida= await _partidaRepository.GetByIdAsync(partidaId, cancellationToken);

        if(partida is null)
        {
        return Result.Failure<PagedResults<PartidaRecursoTenantDto>?>(PartidaTenantErrors.PartidaNotFound);

        }

        var predicateB = PredicateBuilder.New<PartidaRecursoTenant>(p => p.Activo == new Activo(true) && p.PartidaId == partidaId);


        if (!string.IsNullOrEmpty(request.Search))
        {
            var searchPredicate = PredicateBuilder.New<PartidaRecursoTenant>(false);
            

            // searchPredicate = searchPredicate.Or(p => p.NumeroDocumento!.Contains(request.Search));
            // searchPredicate = searchPredicate.Or(p => p.Nombre!.Contains(request.Search));
            

            predicateB = predicateB.And(searchPredicate);
        }


        var resultPagination = await _paginationRecursosRepository.GetPaginationAsync(
                                        predicateB,
                                        x => x.Include(x => x.Recurso)!,
                                        request.PageNumber,
                                        request.PageSize,
                                        request.OrderBy!,
                                        request.OrderAsc);

        var resultsDto = _mapper.Map<List<PartidaRecursoTenantDto>>(resultPagination.Results);

        return new PagedResults<PartidaRecursoTenantDto>
        {
            PageNumber = resultPagination.PageNumber,
            PageSize = resultPagination.PageSize,
            TotalNumberOfPages = resultPagination.TotalNumberOfPages,
            TotalNumberOfRecords = resultPagination.TotalNumberOfRecords,
            Results = resultsDto
        };
    }
}