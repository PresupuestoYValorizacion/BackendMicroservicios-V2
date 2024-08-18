
using MsAcceso.Domain.Entity;
using MsAcceso.Domain.Repository;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class TenantRepository : RepositoryTenant<TenantEntity,TenantId>, ITenantRepository
{
    public TenantRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }

}