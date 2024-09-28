using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.UsersTenant;

namespace MsAcceso.Application.Tenant.Paginations;

public interface IPaginationUsersTenantRepository
{
    Task<PagedResults<UserTenant,UserTenantId>> GetPaginationAsync(
        Expression<Func<UserTenant,bool>> predicate,
        Func<IQueryable<UserTenant>, IIncludableQueryable<UserTenant,object>> includes,
        int page,
        int pageSize,
        string orderBy,
        bool ascending,
        bool disableTracking = true 
    );
}