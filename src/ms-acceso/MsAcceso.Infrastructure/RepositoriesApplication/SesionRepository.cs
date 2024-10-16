
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Sesiones;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class SesionRepository : RepositoryApplication<Sesion, SesionId>, ISesionRepository
{
    public SesionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Sesion?> GetByUserId(string userId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Sesion>()
                    .Where(x => x.UserId == userId && x.Activo == new Activo(true))
                    .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Sesion?> GetSessionByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Sesion>()
                    .Where(x => x.JwtToken == token && x.Activo == new Activo(true))
                    .FirstOrDefaultAsync(cancellationToken);
    }
}