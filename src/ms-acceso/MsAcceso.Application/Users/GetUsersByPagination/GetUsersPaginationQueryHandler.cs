
using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Paginations;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Users.GetUsersByPagination;

internal sealed class GetUsersByPaginationQueryHandler : IQueryHandler<GetUsersByPaginationQuery, PagedResults<UserDto>?>
{
    private readonly IPaginationUserRepository _paginationUserRepository;
    private readonly IMapper _mapper;
    public GetUsersByPaginationQueryHandler(IPaginationUserRepository paginationUserRepository, IMapper mapper)
    {
        _paginationUserRepository = paginationUserRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResults<UserDto>?>> Handle(GetUsersByPaginationQuery request, CancellationToken cancellationToken)
    {

        var predicateB = PredicateBuilder.New<User>(p => p.Activo == new Activo(true));


        if (!string.IsNullOrEmpty(request.Search))
        {
            var searchPredicate = PredicateBuilder.New<User>(false);


            searchPredicate = searchPredicate.Or(p => p.Email!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(p => p.Username!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(p => p.Empresa!.NumeroDocumento!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(p => p.Empresa!.PersonaNatural!.NombreCompleto!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(p => p.Empresa!.PersonaJuridica!.RazonSocial!.Contains(request.Search));
            searchPredicate = searchPredicate.Or(p => p.Rol!.Nombre!.Contains(request.Search));

            predicateB = predicateB.And(searchPredicate);
        }

        var resultPagination = await _paginationUserRepository.GetPaginationAsync(
                                        predicateB,
                                        p =>
                                        p.Include(x => x.Empresa!)
                                        .ThenInclude(x => x.TipoDocumento!)
                                        .Include(x => x.Empresa!)
                                        .ThenInclude(x => x.Tipo!)
                                        .Include(x => x.Empresa!)
                                        .ThenInclude(x => x.PersonaNatural)
                                        .Include(x => x.Empresa!)
                                        .ThenInclude(x => x.PersonaJuridica)
                                        .Include(x => x.Rol)!
                                        ,
                                        request.PageNumber,
                                        request.PageSize,
                                        request.OrderBy!,
                                        request.OrderAsc);


        var resultsDto = _mapper.Map<List<UserDto>>(resultPagination.Results);

        return new PagedResults<UserDto>
        {
            PageNumber = resultPagination.PageNumber,
            PageSize = resultPagination.PageSize,
            TotalNumberOfPages = resultPagination.TotalNumberOfPages,
            TotalNumberOfRecords = resultPagination.TotalNumberOfRecords,
            Results = resultsDto
        };

    }
}