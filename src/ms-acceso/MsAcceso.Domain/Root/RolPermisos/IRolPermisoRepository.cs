
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Domain.Root.RolPermisos;

public interface IRolPermisoRepository
{

    Task<RolPermiso?> GetByIdAsync(RolPermisoId id, CancellationToken cancellationToken = default);
    Task<RolPermiso?> GetByMenuAndRol(SistemaId menuId, RolId rolId ,  CancellationToken cancellationToken = default);

    void Add(RolPermiso rolPermiso);

    void Update(RolPermiso rolPermiso);
    
    void Delete(RolPermiso rolPermiso);

    

}