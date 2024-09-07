using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.RolPermisos;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class RolPermisoRepository : RepositoryApplication<RolPermiso, RolPermisoId>, IRolPermisoRepository
{
    public RolPermisoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<RolPermiso?> GetByMenuAndRol(SistemaId menuId, RolId rolId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<RolPermiso>()
            .Where(x => x.RolId == rolId && x.MenuId == menuId && x.Activo == new Activo(true))
            .FirstOrDefaultAsync(cancellationToken);
    }
}