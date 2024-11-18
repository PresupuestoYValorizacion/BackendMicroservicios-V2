

using MsAcceso.Domain.Tenant.UbigeosTenant;

namespace MsAcceso.Domain.Tenant.UbigeosTenant;

public interface IUbigeoTenantRepository
{
    void Add(UbigeoTenant ubigeo);
    void Update(UbigeoTenant ubigeo);
    void Delete(UbigeoTenant ubigeo);
    Task<List<UbigeoTenant>> GetAllUbigeosBySubnivel(UbigeoTenantId Id, CancellationToken cancellationToken);
    // Task<UbigeoTenant?> GetByIdAsync(UbigeoId ubigeoId, CancellationToken cancellationToken);
    // Task<bool> UbigeoExists(string ubigeoNombre,int nivel, CancellationToken cancellationToken = default);
    // Task<bool> ValorExists(string valor,int dependencia, CancellationToken cancellationToken = default);
    // Task<int> GetLastUbigeoIdAsync(CancellationToken cancellationToken = default);
    // Task<List<UbigeoTenant>> GetRelatedEntitiesAsync(int ubigeoId, CancellationToken cancellationToken);
    // Task<List<UbigeoTenant>> GetAllUbigeosBySubnivelToDelete(UbigeoId Id, CancellationToken cancellationToken);

}
