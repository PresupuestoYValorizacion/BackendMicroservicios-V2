using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Paginations;

public interface IPaginationParametrosRepository
{
    Task<PagedResults<Parametro,ParametroId>> GetPaginationAsync(
        Expression<Func<Parametro,bool>> predicate,
        Func<IQueryable<Parametro>, IIncludableQueryable<Parametro,object>> includes,
        int page,
        int pageSize,
        string orderBy,
        bool ascending,
        bool disableTracking = true 
    );
}