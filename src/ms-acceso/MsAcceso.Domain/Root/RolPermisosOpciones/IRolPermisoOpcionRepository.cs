

using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.RolPermisos;

namespace MsAcceso.Domain.Root.RolPermisosOpciones;

public interface IRolPermisoOpcionRepository
{

    Task<RolPermisoOpcion?> GetByIdAsync(RolPermisoOpcionId id, CancellationToken cancellationToken = default);

    Task<RolPermisoOpcion?> GetByOpcionAndRolPermiso(RolPermisoId rolPermisoId, OpcionId opcionId ,  CancellationToken cancellationToken = default);

    void Add(RolPermisoOpcion user);

    void Update(RolPermisoOpcion user);
    
    void Delete(RolPermisoOpcion user);

    

}