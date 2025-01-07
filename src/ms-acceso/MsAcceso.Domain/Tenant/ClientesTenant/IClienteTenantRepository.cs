

namespace MsAcceso.Domain.Tenant.ClientesTenant;

public interface IClienteTenantRepository
{

    Task<ClienteTenant?> GetByIdAsync(ClienteTenantId id, CancellationToken cancellationToken = default);
    void Add(ClienteTenant clienteTenant);
    void Update(ClienteTenant clienteTenant);
    void Delete(ClienteTenant clienteTenant);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<ClienteTenant?> GetByNumeroDocumento(string numeroDocumento, CancellationToken cancellationToken = default);
    Task<List<ClienteTenant>> GetAllAsync(CancellationToken cancellationToken = default);


}