using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.RolPermisos;
using MsAcceso.Domain.Root.RolPermisosOpciones;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class RolPermisoOpcionRepository : RepositoryApplication<RolPermisoOpcion, RolPermisoOpcionId>, IRolPermisoOpcionRepository
{
    public RolPermisoOpcionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<RolPermisoOpcion?> GetByOpcionAndRolPermiso(RolPermisoId rolPermisoId, OpcionId opcionId, CancellationToken cancellationToken = default)
    {
       return await DbContext.Set<RolPermisoOpcion>()
            .Where(x => x.RolPermisoId == rolPermisoId && x.OpcionId == opcionId && x.Activo == new Activo(true))
            .FirstOrDefaultAsync(cancellationToken);
    }
}