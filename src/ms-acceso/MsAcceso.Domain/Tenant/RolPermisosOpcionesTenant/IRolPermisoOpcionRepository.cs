

using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Tenant.RolPermisosTenant;

namespace MsAcceso.Domain.Tenant.RolPermisosOpcionesTenant;

public interface IRolPermisoOpcionTenantRepository
{

    Task<RolPermisoOpcionTenant?> GetByIdAsync(RolPermisoOpcionTenantId id, CancellationToken cancellationToken = default);

    Task<RolPermisoOpcionTenant?> GetByOpcionAndRolPermiso(RolPermisoTenantId rolPermisoId, OpcionId opcionId ,  CancellationToken cancellationToken = default);

    void Add(RolPermisoOpcionTenant user);

    void Update(RolPermisoOpcionTenant user);
    
    void Delete(RolPermisoOpcionTenant user);

    

}