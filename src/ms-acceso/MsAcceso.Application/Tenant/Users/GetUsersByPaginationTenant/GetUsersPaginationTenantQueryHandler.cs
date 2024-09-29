
using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Tenant.Paginations;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.UsersTenant;
namespace MsAcceso.Application.Tenant.Users.GetUsersByPaginationTenant;

internal sealed class GetUsersByPaginationTenantQueryHandler : IQueryHandler<GetUsersByPaginationTenantQuery, PagedResults<UserTenantDto>?>
{
    private readonly IPaginationUsersTenantRepository _paginationUserRepository;
    private readonly IParametroRepository _parametroRepository;
    private readonly IMapper _mapper;
    public GetUsersByPaginationTenantQueryHandler(IPaginationUsersTenantRepository paginationUserRepository,IParametroRepository parametroRepository, IMapper mapper)
    {
        _paginationUserRepository = paginationUserRepository;
        _parametroRepository = parametroRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResults<UserTenantDto>?>> Handle(GetUsersByPaginationTenantQuery request, CancellationToken cancellationToken)
    {

        var predicateB = PredicateBuilder.New<UserTenant>(p => p.Activo == new Activo(true));


        if (!string.IsNullOrEmpty(request.Search))
        {
            var searchPredicate = PredicateBuilder.New<UserTenant>(false);


            searchPredicate = searchPredicate.Or(p => p.Email!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(p => p.Username!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(p => p.Persona!.NumeroDocumento!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(p => p.Persona!.PersonaNatural!.NombreCompleto!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(p => p.Persona!.PersonaJuridica!.RazonSocial!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(p => p.Rol!.Nombre!.Contains(request.Search));

            predicateB = predicateB.And(searchPredicate);
        }

        var resultPagination = await _paginationUserRepository.GetPaginationAsync(
                                        predicateB,
                                        p =>
                                        p.Include(x => x.Persona!)
                                        .ThenInclude(x => x.PersonaNatural)
                                        .Include(x => x.Persona!)
                                        .ThenInclude(x => x.PersonaJuridica)
                                        .Include(x => x.Rol)!
                                        ,
                                        request.PageNumber,
                                        request.PageSize,
                                        request.OrderBy!,
                                        request.OrderAsc);
        
        var tipoPersonas = await _parametroRepository.GetRelatedEntitiesAsync( ParametroEnum.TipoPersona, cancellationToken);

        foreach(var user in resultPagination.Results)
        {  

            user.Persona!.Tipo = tipoPersonas.FirstOrDefault(x => x.Id == new ParametroId(user.Persona.TipoId ?? 0));
            user.Persona!.TipoDocumento = await _parametroRepository.GetByIdAsync(new ParametroId(user.Persona.TipoDocumentoId ?? 0), cancellationToken);

        }


        var resultsDto = _mapper.Map<List<UserTenantDto>>(resultPagination.Results);

        return new PagedResults<UserTenantDto>
        {
            PageNumber = resultPagination.PageNumber,
            PageSize = resultPagination.PageSize,
            TotalNumberOfPages = resultPagination.TotalNumberOfPages,
            TotalNumberOfRecords = resultPagination.TotalNumberOfRecords,
            Results = resultsDto
        };

    }
}