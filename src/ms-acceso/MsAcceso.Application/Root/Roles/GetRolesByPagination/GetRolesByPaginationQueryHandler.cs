using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Root.Paginations;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Root.Roles.GetRolesByPagination;

internal sealed class GetRolesByPaginationQueryHandler : IQueryHandler<GetRolesByPaginationQuery, PagedResults<RolDto>?>
{
    private readonly IPaginationRolesRepository _paginationRolesRepository;
    private readonly IMapper _mapper;

    public GetRolesByPaginationQueryHandler(
        IPaginationRolesRepository paginationRolesRepository,
        IMapper mapper
    ) 
    {
        _paginationRolesRepository = paginationRolesRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResults<RolDto>?>> Handle(GetRolesByPaginationQuery request, CancellationToken cancellationToken)
    {

        var predicateB = PredicateBuilder.New<Rol>(p => p.Activo == new Activo(true));

        if(!string.IsNullOrEmpty(request.Search))
        {
            var searchPredicate = PredicateBuilder.New<Rol>(false);

            searchPredicate = searchPredicate.Or(o => o.Nombre!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(o => o.Licencia!.Nombre!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(o => o.TipoRol!.Nombre!.Contains(request.Search));

            predicateB = predicateB.And(searchPredicate);
        }

        var resultPagination = await _paginationRolesRepository.GetPaginationAsync(
                                            predicateB,
                                            r =>
                                            r.Include(x => x.Licencia!)
                                            .Include(x => x.TipoRol!)
                                            ,
                                            request.PageNumber,
                                            request.PageSize,
                                            request.OrderBy!,
                                            request.OrderAsc
                                        );

        var resultsDto = _mapper.Map<List<RolDto>>(resultPagination.Results);

        return new PagedResults<RolDto>
        {
            PageNumber = resultPagination.PageNumber,
            PageSize = resultPagination.PageSize,
            TotalNumberOfPages = resultPagination.TotalNumberOfPages,
            TotalNumberOfRecords = resultPagination.TotalNumberOfRecords,
            Results = resultsDto
        };
    }
}