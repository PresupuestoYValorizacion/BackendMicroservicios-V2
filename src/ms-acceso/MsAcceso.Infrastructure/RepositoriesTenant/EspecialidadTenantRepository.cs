using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class EspecialidadTenantRepository : RepositoryTenant<EspecialidadTenant, EspecialidadTenantId>, IEspecialidadTenantRepository
{

    public EspecialidadTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<List<EspecialidadTenant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<EspecialidadTenant>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

    public async Task<bool> EspecialidadTenantExist(string nombreEspecialidad, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<EspecialidadTenant>().AnyAsync(x => x.Nombre == nombreEspecialidad && x.Activo == new Activo(true), cancellationToken);
    }
}