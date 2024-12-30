namespace MsAcceso.Domain.Tenant.ProyectosTenant;

public interface IProyectoTenantRepository
{

    void Add(ProyectoTenant especialidad);
    void Update(ProyectoTenant especialidad);
    void Delete(ProyectoTenant especialidad);
    Task<ProyectoTenant?> GetByIdAsync(ProyectoTenantId especialidadId, CancellationToken cancellationToken = default);
    Task<ProyectoTenant?> GetByNombre(string nombre, CancellationToken cancellationToken = default);
    Task<bool> ProyectoTenantExist(string nombreEspecialidad, CancellationToken cancellationToken = default);
    Task<List<ProyectoTenant>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<ProyectoTenant>> GetAllAsyncWithIncludes(CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}