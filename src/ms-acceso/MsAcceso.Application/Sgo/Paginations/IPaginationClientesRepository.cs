using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Application.Sgo.Paginations;

public interface IPaginationClientesRepository
{
    Task<PagedResults<ClienteTenant,ClienteTenantId>> GetPaginationAsync(
        Expression<Func<ClienteTenant,bool>> predicate,
        Func<IQueryable<ClienteTenant>, IIncludableQueryable<ClienteTenant,object>> includes,
        int page,
        int pageSize,
        string orderBy,
        bool ascending,
        bool disableTracking = true 
    );
}