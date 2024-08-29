
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Shared;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class LicenciaRepository : RepositoryTenant<Licencia, LicenciaId>, ILicenciaRepository
{
    public LicenciaRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Licencia>> GetAll(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Licencia>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

    public async Task<Licencia?> GetByNombre(string nombre, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Licencia>().Where(x => x.Nombre == nombre).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Licencia>> GetAllActive(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Licencia>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

    public async Task<bool> LicenciaExists(string licenciaNombre, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Licencia>().AnyAsync(x => x.Nombre == licenciaNombre, cancellationToken);
    }
    
}