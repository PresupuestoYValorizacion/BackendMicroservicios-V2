namespace MsAcceso.Domain.Tenant.UbigeosTenant;

public interface IUbigeoTenantRepository
{
    void Add(UbigeoTenant ubigeo);
    void Update(UbigeoTenant ubigeo);
    void Delete(UbigeoTenant ubigeo);
    // Task<List<UbigeoTenant>> GetAllUbigeosBySubnivel(UbigeoTenantId Id, CancellationToken cancellationToken);
    Task<UbigeoTenant?> GetByIdAsync(UbigeoTenantId ubigeoId, CancellationToken cancellationToken);
    Task<bool> UbigeoExistsByName(string ubigeoNombre, CancellationToken cancellationToken = default);
    Task<List<UbigeoTenant>> GetAllAsync(CancellationToken cancellationToken);
}
