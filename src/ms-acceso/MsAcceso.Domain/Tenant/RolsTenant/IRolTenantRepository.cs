
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Domain.Tenant.RolsTenant;

public interface IRolTenantRepository
{

    Task<RolTenant?> GetByIdAsync(RolTenantId id, CancellationToken cancellationToken = default);
    Task<bool> GetByNombreAsync(string nombre, CancellationToken cancellationToken = default);

    void Add(RolTenant user);

    void Update(RolTenant user);
    
    void Delete(RolTenant user);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<bool> ValidarAcceso(RolTenantId id, string sistemaId, CancellationToken cancellationToken = default);


    

}