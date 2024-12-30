
namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTenant;

public interface IPresupuestoEspecialidadTenantRepository
{

    void Add(PresupuestoEspecialidadTenant especialidad);
    void Update(PresupuestoEspecialidadTenant especialidad);
    void Delete(PresupuestoEspecialidadTenant especialidad);
    Task<PresupuestoEspecialidadTenant?> GetByIdAsync(PresupuestoEspecialidadTenantId especialidadId, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}