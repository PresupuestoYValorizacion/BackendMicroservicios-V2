namespace MsAcceso.Domain.Tenant.TitulosTenant;

public interface ITituloTenantRepository
{

    void Add(TituloTenant especialidad);
    void Update(TituloTenant especialidad);
    void Delete(TituloTenant especialidad);
    Task<TituloTenant?> GetByIdAsync(TituloTenantId especialidadId, CancellationToken cancellationToken = default);
    Task<bool> TituloTenantExist(string nombreTituloTenant, CancellationToken cancellationToken = default);
    Task<List<TituloTenant>> GetAllAsync(CancellationToken cancellationToken);
    
}