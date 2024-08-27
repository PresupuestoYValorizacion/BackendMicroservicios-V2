
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Tenant.Users;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class UserTenantRepository : RepositoryTenant<User,UserId>, IUserTenantRepository
{
    public UserTenantRepository(EnterpriseDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<User>()
                    .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<bool> IsUserExists(string email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<User>()
                    .AnyAsync(x => x.Email == email,cancellationToken);
    }

    public async Task<bool> ValidateIdUsuarioExists(Guid idUsuario, CancellationToken cancellationToken = default)
    {
       return await DbContext.Set<User>()
                    .AnyAsync(x => x.Id == new UserId(idUsuario),cancellationToken);
    }
}