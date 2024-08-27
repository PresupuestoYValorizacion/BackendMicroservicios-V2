
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Root.UsuarioLicencias;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class UsuarioLicenciaRepository : RepositoryApplication<UsuarioLicencia, UsuarioLicenciaId>, IUsuarioLicenciaRepository
{
    public UsuarioLicenciaRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

     public async Task<UsuarioLicencia?> GetByUserAsync(UserId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<UsuarioLicencia>()
             .FirstOrDefaultAsync(x => x.UserId == id && x.Activo == new Activo(true), cancellationToken);
    }


}