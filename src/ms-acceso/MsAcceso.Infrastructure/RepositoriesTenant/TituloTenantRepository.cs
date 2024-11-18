using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Shared;
//using MsAcceso.Application.Root.Paginations;
using MsAcceso.Domain.Tenant.TitulosTenant;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class TituloTenantRepository : RepositoryApplication<TituloTenant, TituloTenantId>, ITituloTenantRepository//, IPaginationTituloTenantRepository
{

    public TituloTenantRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<List<TituloTenant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<TituloTenant>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

    public async Task<bool> TituloTenantExist(string nombreTituloTenant, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<TituloTenant>().AnyAsync(x => x.Nombre == nombreTituloTenant && x.Activo == new Activo(true), cancellationToken);
    }
}