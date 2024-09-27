using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Root.Paginations;
using MsAcceso.Application.Tenant.Paginations;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Roles.GetRolesByPaginationTenant;

internal sealed class GetRolesByPaginationTenantQueryHandler : IQueryHandler<GetRolesByPaginationTenantQuery, PagedResults<RolTenantDto>?>
{
    private readonly IPaginationRolesTenantRepository _paginationRolesTenantRepository;
    private readonly IMapper _mapper;

    public GetRolesByPaginationTenantQueryHandler(
        IPaginationRolesTenantRepository paginationRolesTenantRepository,
        IMapper mapper
    )
    {
        _paginationRolesTenantRepository = paginationRolesTenantRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResults<RolTenantDto>?>> Handle(GetRolesByPaginationTenantQuery request, CancellationToken cancellationToken)
    {

        var predicateB = PredicateBuilder.New<RolTenant>(p => p.Activo == new Activo(true));

        if(!string.IsNullOrEmpty(request.Search))
        {
            var searchPredicate = PredicateBuilder.New<RolTenant>(false);

            searchPredicate = searchPredicate.Or(o => o.Nombre!.Contains(request.Search));

            predicateB = predicateB.And(searchPredicate);
        }

        var resultPagination = await _paginationRolesTenantRepository.GetPaginationAsync(
                                            predicateB,
                                            null!,
                                            request.PageNumber,
                                            request.PageSize,
                                            request.OrderBy!,
                                            request.OrderAsc
                                        );

        var resultsDto = _mapper.Map<List<RolTenantDto>>(resultPagination.Results);

        return new PagedResults<RolTenantDto>
        {
            PageNumber = resultPagination.PageNumber,
            PageSize = resultPagination.PageSize,
            TotalNumberOfPages = resultPagination.TotalNumberOfPages,
            TotalNumberOfRecords = resultPagination.TotalNumberOfRecords,
            Results = resultsDto
        };
    }
}