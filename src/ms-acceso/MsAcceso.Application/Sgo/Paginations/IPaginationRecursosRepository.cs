using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.ClientesTenant;
using MsAcceso.Domain.Tenant.PartidasRecursosTenant;
using MsAcceso.Domain.Tenant.RecursosTenant;

namespace MsAcceso.Application.Sgo.Paginations;

public interface IPaginationRecursosRepository
{
    Task<PagedResults<PartidaRecursoTenant,PartidaRecursoTenantId>> GetPaginationAsync(
        Expression<Func<PartidaRecursoTenant,bool>> predicate,
        Func<IQueryable<PartidaRecursoTenant>, IIncludableQueryable<PartidaRecursoTenant,object>> includes,
        int page,
        int pageSize,
        string orderBy,
        bool ascending,
        bool disableTracking = true 
    );
}