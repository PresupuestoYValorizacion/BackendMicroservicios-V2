using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Shared;
//using MsAcceso.Application.Root.Paginations;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class EspecialidadTenantRepository : RepositoryApplication<EspecialidadTenant, EspecialidadTenantId>, IEspecialidadTenantRepository//, IPaginationEspecialidadTenantRepository
{

    public EspecialidadTenantRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<List<EspecialidadTenant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<EspecialidadTenant>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

    public async Task<bool> EspecialidadTenantExist(string nombreEspecialidadTenant, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<EspecialidadTenant>().AnyAsync(x => x.Nombre == nombreEspecialidadTenant && x.Activo == new Activo(true), cancellationToken);
    }
}