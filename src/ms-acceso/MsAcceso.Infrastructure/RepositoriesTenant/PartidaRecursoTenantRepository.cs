using MsAcceso.Application.Sgo.Paginations;
using MsAcceso.Domain.Tenant.PartidasRecursosTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PartidaRecursoTenantRepository : RepositoryTenant<PartidaRecursoTenant, PartidaRecursoTenantId>, IPartidaRecursoTenantRepository, IPaginationRecursosRepository
{
    public PartidaRecursoTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

}