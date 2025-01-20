using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Sgo.Paginations;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PartidasRecursosTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PartidaRecursoTenantRepository : RepositoryTenant<PartidaRecursoTenant, PartidaRecursoTenantId>, IPartidaRecursoTenantRepository, IPaginationRecursosRepository
{
    public PartidaRecursoTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<PartidaRecursoTenant?> GetByIdWithIncludesAsync(PartidaRecursoTenantId partidaRecursoId, CancellationToken cancellationToken = default)
    {
       return await DbContext.Set<PartidaRecursoTenant>()
                                                   .Where(x => x.Activo == new Activo(true) && x.Id == partidaRecursoId )
                                                   .Include(x => x.Recurso)
                                                   .FirstOrDefaultAsync(cancellationToken);
    }
}