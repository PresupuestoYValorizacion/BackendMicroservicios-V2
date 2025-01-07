
namespace MsAcceso.Domain.Tenant.PresupuestosTenant;

public interface IPresupuestoTenantRepository
{

    void Add(PresupuestoTenant especialidad);
    void Update(PresupuestoTenant especialidad);
    void Delete(PresupuestoTenant especialidad);
    Task<PresupuestoTenant?> GetByIdAsync(PresupuestoTenantId especialidadId, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}