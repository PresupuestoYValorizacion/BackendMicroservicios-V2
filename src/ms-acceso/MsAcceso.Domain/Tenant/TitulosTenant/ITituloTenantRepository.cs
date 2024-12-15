namespace MsAcceso.Domain.Tenant.TitulosTenant;

public interface ITituloTenantRepository
{

    void Add(TituloTenant titulo);
    void Update(TituloTenant titulo);
    void Delete(TituloTenant titulo);
    Task<TituloTenant?> GetByIdAsync(TituloTenantId tituloId, CancellationToken cancellationToken = default);
    Task<bool> TituloExist(string nombreTitulo, CancellationToken cancellationToken = default);
    Task<List<TituloTenant>> GetAllAsync(CancellationToken cancellationToken);
    
}