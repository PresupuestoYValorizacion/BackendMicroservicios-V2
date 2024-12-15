namespace MsAcceso.Domain.Tenant.EspecialidadesTenant;

public interface IEspecialidadTenantRepository
{

    void Add(EspecialidadTenant especialidad);
    void Update(EspecialidadTenant especialidad);
    void Delete(EspecialidadTenant especialidad);
    Task<EspecialidadTenant?> GetByIdAsync(EspecialidadTenantId especialidadId, CancellationToken cancellationToken = default);
    Task<bool> EspecialidadTenantExist(string nombreEspecialidad, CancellationToken cancellationToken = default);
    Task<List<EspecialidadTenant>> GetAllAsync(CancellationToken cancellationToken);
    
}