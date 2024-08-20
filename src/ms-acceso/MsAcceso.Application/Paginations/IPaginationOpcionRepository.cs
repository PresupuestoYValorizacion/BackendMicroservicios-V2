using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Paginations;

public interface IPaginationOpcionRepository
{

    Task<PagedResults<Opcion,OpcionId>> GetPaginationAsync(
        Expression<Func<Opcion,bool>> predicate,
        Func<IQueryable<Opcion>, IIncludableQueryable<Opcion,object>> includes,
        int page,
        int pageSize,
        string orderBy,
        bool ascending,
        bool disableTracking = true 
    );
}