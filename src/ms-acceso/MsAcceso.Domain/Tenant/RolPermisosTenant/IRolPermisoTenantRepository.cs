
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Domain.Tenant.RolPermisosTenant;

public interface IRolPermisoTenantRepository
{

    Task<RolPermisoTenant?> GetByIdAsync(RolPermisoTenantId id, CancellationToken cancellationToken = default);
    Task<RolPermisoTenant?> GetByMenuAndRol(SistemaId menuId, RolId rolId ,  CancellationToken cancellationToken = default);

    void Add(RolPermisoTenant rolPermiso);

    void Update(RolPermisoTenant rolPermiso);
    
    void Delete(RolPermisoTenant rolPermiso);

    

}