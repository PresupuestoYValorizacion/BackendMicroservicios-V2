using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.ProyectosTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class ProyectoTenantRepository : RepositoryTenant<ProyectoTenant, ProyectoTenantId>, IProyectoTenantRepository
{

    public ProyectoTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<List<ProyectoTenant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<ProyectoTenant>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

    public async Task<List<ProyectoTenant>> GetAllAsyncWithIncludes(CancellationToken cancellationToken)
    {
       return await DbContext.Set<ProyectoTenant>().Where(x => x.Activo == new Activo(true))
                    .Include(x => x.Especialidades!)
                    .Include(x => x.Presupuesto!)
                    .ToListAsync(cancellationToken);

    }

    public async Task<ProyectoTenant?> GetByNombre(string nombre, CancellationToken cancellationToken = default)
    {   
        return await DbContext.Set<ProyectoTenant>().Where(x => x.Activo == new Activo(true) && x.Nombre == nombre).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> ProyectoTenantExist(string nombreProyecto, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<ProyectoTenant>().AnyAsync(x => x.Nombre == nombreProyecto && x.Activo == new Activo(true), cancellationToken);
    }
}