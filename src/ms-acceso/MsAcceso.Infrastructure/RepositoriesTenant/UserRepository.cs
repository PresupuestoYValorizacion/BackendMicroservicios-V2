
using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Paginations;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Infrastructure.RepositoriesTenant;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.Repositories;

internal sealed class UserRepository : RepositoryTenant<User,UserId>, IUserRepository, IPaginationUserRepository
{
    public UserRepository(TenantDbContext dbContext) : base(dbContext)
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

    public async Task<List<User>> GetAll(
        CancellationToken cancellationToken = default
    )
    {
        return await DbContext.Set<User>().Include(e => e.Empresa).ThenInclude(e => e!.TipoDocumento).Include(u => u.Empresa)
                .ThenInclude(e => e!.Tipo).ToListAsync(cancellationToken);
    }
}