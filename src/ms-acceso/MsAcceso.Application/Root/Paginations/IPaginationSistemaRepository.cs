using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Root.Paginations;

public interface IPaginationSistemaRepository
{
    Task<PagedResults<Sistema,SistemaId>> GetPaginationAsync(
        Expression<Func<Sistema,bool>> predicate,
        Func<IQueryable<Sistema> , IIncludableQueryable<Sistema,object>> includes,
        int page,
        int pageSize,
        string orderBy,
        bool ascending,
        bool disableTracking = true
    );
}
