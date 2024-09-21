using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Paginations;

public interface IPaginationRolesTenantRepository
{
    Task<PagedResults<RolTenant,RolTenantId>> GetPaginationAsync(
        Expression<Func<RolTenant,bool>> predicate,
        Func<IQueryable<RolTenant>, IIncludableQueryable<RolTenant,object>> includes,
        int page,
        int pageSize,
        string orderBy,
        bool ascending,
        bool disableTracking = true 
    );
}