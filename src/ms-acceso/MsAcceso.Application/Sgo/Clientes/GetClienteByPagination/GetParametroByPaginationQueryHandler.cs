using AutoMapper;
using LinqKit;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Sgo.Paginations;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Application.Sgo.Clientes.GetClienteByPagination;

internal sealed class GetClienteByPaginationQueryHandler : IQueryHandler<GetClienteByPaginationQuery, PagedResults<ClienteDto>?>
{
    private readonly IPaginationClientesRepository _paginationClientesRepository;
    private readonly IParametroRepository _parametroRepository;
    private readonly IMapper _mapper;

    public GetClienteByPaginationQueryHandler(
        IPaginationClientesRepository paginationClientesRepository,
        IParametroRepository parametroRepository,
        IMapper mapper
    )
    {
        _paginationClientesRepository = paginationClientesRepository;
        _parametroRepository = parametroRepository;
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

         var tipoPersonas = await _parametroRepository.GetRelatedEntitiesAsync( ParametroEnum.TipoPersona, cancellationToken);
         var tipoClientes = await _parametroRepository.GetRelatedEntitiesAsync( ParametroEnum.TipoCliente, cancellationToken);

        foreach(var cliente in resultPagination.Results)
        {  

            cliente.TipoPersona = tipoPersonas.FirstOrDefault(x => x.Id == new ParametroId(cliente.TipoPersonaId ?? 0));
            cliente.TipoDocumento = await _parametroRepository.GetByIdAsync(new ParametroId(cliente.TipoDocumentoId ?? 0), cancellationToken);
            cliente.TipoCliente = tipoClientes.FirstOrDefault(x => x.Id == new ParametroId(cliente.TipoClienteId ?? 0));

        }

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