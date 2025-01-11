
using MsAcceso.Domain.Tenant.EspecialidadesTenant;

namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;

public interface IPresupuestoEspecialidadTituloTenantRepository
{

    void Add(PresupuestoEspecialidadTituloTenant especialidad);
    void Update(PresupuestoEspecialidadTituloTenant especialidad);
    void Delete(PresupuestoEspecialidadTituloTenant especialidad);
    Task<PresupuestoEspecialidadTituloTenant?> GetByIdAsync(PresupuestoEspecialidadTituloTenantId especialidadId, CancellationToken cancellationToken = default);
    Task<string?> GetLastCorrelativoAsync(EspecialidadTenantId especialidadId, CancellationToken cancellationToken = default);
    Task<List<PresupuestoEspecialidadTituloTenant>> GetAllAsyncWithIncludes(EspecialidadTenantId especialidadId, CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}