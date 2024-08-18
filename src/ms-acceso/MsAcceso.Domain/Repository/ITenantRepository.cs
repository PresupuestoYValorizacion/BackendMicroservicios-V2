
using MsAcceso.Domain.Entity;

namespace MsAcceso.Domain.Repository;

public interface ITenantRepository
{

    Task<TenantEntity?> GetByIdAsync(TenantId id, CancellationToken cancellationToken = default);

    void Add(TenantEntity user);

    void Update(TenantEntity user);
    
    void Delete(TenantEntity user);

    

}