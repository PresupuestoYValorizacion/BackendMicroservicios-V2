
namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;

public interface IPresupuestoEspecialidadTituloTenantRepository
{

    void Add(PresupuestoEspecialidadTituloTenant especialidad);
    void Update(PresupuestoEspecialidadTituloTenant especialidad);
    void Delete(PresupuestoEspecialidadTituloTenant especialidad);
    Task<PresupuestoEspecialidadTituloTenant?> GetByIdAsync(PresupuestoEspecialidadTituloTenantId especialidadId, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}