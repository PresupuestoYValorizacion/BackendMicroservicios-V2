
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class LicenciaRepository : RepositoryTenant<Licencia, LicenciaId>, ILicenciaRepository
{
    public LicenciaRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Licencia>> GetAll(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Licencia>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }
}