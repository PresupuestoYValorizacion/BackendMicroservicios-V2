
using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Paginations;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Users;

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

        var predicateb = PredicateBuilder.New<User>(true);

        if(!string.IsNullOrEmpty(request.Search))
        {
            predicateb = predicateb.Or(p => p.Email == request.Search);
        }
        var resultPagination = await _paginationUserRepository.GetPaginationAsync(
                                        predicateb, 
                                        p => 
                                        p.Include(x => x.Empresa!)
                                        .ThenInclude(x => x.TipoDocumento!)
                                        .Include(x => x.Empresa!)
                                        .ThenInclude(x => x.Tipo!)
                                        .Include(x => x.Empresa!)
                                        .ThenInclude(x => x.PersonaNatural!)
                                        .Include(x => x.Empresa!)
                                        .ThenInclude(x => x.PersonaJuridica!)
                                        ,
                                        request.PageNumber, 
                                        request.PageSize, 
                                        request.OrderBy!, 
                                        request.OrderAsc);


        var resultsDto = _mapper.Map<List<UserDto>>(resultPagination.Results);

        return new PagedResults<UserDto>{
            PageNumber = resultPagination.PageNumber ,
            PageSize = resultPagination.PageSize ,
            TotalNumberOfPages= resultPagination.TotalNumberOfPages,
            TotalNumberOfRecords = resultPagination.TotalNumberOfRecords,
            Results = resultsDto
        };

    }
}