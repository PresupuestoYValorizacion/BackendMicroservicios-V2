
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Domain.Tenant.RolPermisosTenant;

public interface IRolPermisoTenantRepository
{

    Task<RolPermisoTenant?> GetByIdAsync(RolPermisoTenantId id, CancellationToken cancellationToken = default);
    Task<RolPermisoTenant?> GetByMenuAndRol(string menuId, RolTenantId rolId ,  CancellationToken cancellationToken = default);

    void Add(RolPermisoTenant rolPermiso);

    void Update(RolPermisoTenant rolPermiso);
    
    void Delete(RolPermisoTenant rolPermiso);

    Task<bool> ValidarPermisoMenu(string menuId, RolTenantId rolId ,  CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    

}