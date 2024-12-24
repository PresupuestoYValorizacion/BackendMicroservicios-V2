
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Domain.Tenant.PersonasTenant;

public interface IClienteTenantRepository
{

    Task<ClienteTenant?> GetByIdAsync(ClienteTenantId id, CancellationToken cancellationToken = default);
    void Add(ClienteTenant clienteTenant);
    void Update(ClienteTenant clienteTenant);
    void Delete(ClienteTenant clienteTenant);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}