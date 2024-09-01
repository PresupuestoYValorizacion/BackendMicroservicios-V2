
using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Paginations;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class RolRepository : RepositoryApplication<Rol, RolId>, IRolRepository, IPaginationRolesRepository
{
    public RolRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Rol?> GetByLicenciaAsync(LicenciaId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Rol>()
             .FirstOrDefaultAsync(x => x.LicenciaId == id && x.Activo == new Activo(true), cancellationToken);
    }

    public async Task<Rol?> GetRolByParametroAndLicencia(ParametroId parametroId, LicenciaId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Rol>()
             .FirstOrDefaultAsync(x => x.TipoRolId == parametroId && x.LicenciaId == id && x.Activo == new Activo(true), cancellationToken);
    }

    public async Task<List<Rol>> GetRolesByTipoAsync(ParametroId TipoId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Rol>().Where(x => x.TipoRolId == TipoId && x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

    public async Task<bool> GetByNombreAsync(string nombre, CancellationToken cancellationToken = default)
    {
         return await DbContext.Set<Rol>()
                    .AnyAsync(x => x.Nombre == nombre && x.Activo == new Activo(true), cancellationToken);

    }
}