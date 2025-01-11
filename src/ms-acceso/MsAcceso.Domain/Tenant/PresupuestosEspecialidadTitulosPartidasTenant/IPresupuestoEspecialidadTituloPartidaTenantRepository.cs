
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;

namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant;

public interface IPresupuestoEspecialidadTituloPartidaTenantRepository
{

    void Add(PresupuestoEspecialidadTituloPartidaTenant especialidad);
    void Update(PresupuestoEspecialidadTituloPartidaTenant especialidad);
    void Delete(PresupuestoEspecialidadTituloPartidaTenant especialidad);
    Task<PresupuestoEspecialidadTituloPartidaTenant?> GetByIdAsync(PresupuestoEspecialidadTituloPartidaTenantId especialidadId, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}