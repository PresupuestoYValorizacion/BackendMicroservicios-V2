using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PresupuestoEspecialidadTenantRepository : RepositoryTenant<PresupuestoEspecialidadTenant, PresupuestoEspecialidadTenantId>, IPresupuestoEspecialidadTenantRepository
{

    public PresupuestoEspecialidadTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<List<PresupuestoEspecialidadTenant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<PresupuestoEspecialidadTenant>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }
    public Task<bool> PresupuestoEspecialidadTenantExist(string nombreEspecialidad, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

}