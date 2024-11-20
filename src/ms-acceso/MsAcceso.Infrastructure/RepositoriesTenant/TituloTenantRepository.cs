using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.TitulosTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class TituloTenantRepository : RepositoryTenant<TituloTenant, TituloTenantId>, ITituloTenantRepository
{

    public TituloTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<List<TituloTenant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<TituloTenant>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

    public async Task<bool> TituloExist(string nombreTitulo, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<TituloTenant>().AnyAsync(x => x.Nombre == nombreTitulo && x.Activo == new Activo(true), cancellationToken);
    }
}