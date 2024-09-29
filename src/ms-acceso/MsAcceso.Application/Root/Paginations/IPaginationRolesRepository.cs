using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Root.Paginations;

public interface IPaginationRolesRepository
{
    Task<PagedResults<Rol,RolId>> GetPaginationAsync(
        Expression<Func<Rol,bool>> predicate,
        Func<IQueryable<Rol>, IIncludableQueryable<Rol,object>> includes,
        int page,
        int pageSize,
        string orderBy,
        bool ascending,
        bool disableTracking = true 
    );
}