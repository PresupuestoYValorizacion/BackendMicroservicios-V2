using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.RecursosTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class RecursoTenantRepository : RepositoryTenant<RecursoTenant, RecursoTenantId>, IRecursoTenantRepository
{

    public RecursoTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<List<RecursoTenant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<RecursoTenant>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

    public async Task<bool> RecursoExist(string nombreRecurso, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<RecursoTenant>().AnyAsync(x => x.Nombre == nombreRecurso && x.Activo == new Activo(true), cancellationToken);
    }
}