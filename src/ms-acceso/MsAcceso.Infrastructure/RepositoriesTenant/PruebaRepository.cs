
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Tenant.Presupuestos;
using MsAcceso.Domain.Tenant.Pruebas;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PruebaTenantRepository : RepositoryTenant<Prueba,PruebaId>, IPruebaTenantRepository
{
    public PruebaTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<List<Prueba>> GetAll(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Prueba>().ToListAsync(cancellationToken);
    }

}