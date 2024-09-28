
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Tenant.UsersTenant;
using MsAcceso.Infrastructure.Service;
using MsAcceso.Application.Tenant.Paginations;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class UserTenantRepository : RepositoryTenant<UserTenant,UserTenantId>, IUserTenantRepository, IPaginationUsersTenantRepository
{
    public UserTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<UserTenant?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<UserTenant>()
                    .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<bool> IsUserExists(string email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<UserTenant>()
                    .AnyAsync(x => x.Email == email,cancellationToken);
    }

    public async Task<bool> ValidateIdUsuarioExists(Guid idUsuario, CancellationToken cancellationToken = default)
    {
       return await DbContext.Set<UserTenant>()
                    .AnyAsync(x => x.Id == new UserTenantId(idUsuario),cancellationToken);
    }
}