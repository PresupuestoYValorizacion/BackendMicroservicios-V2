
namespace MsAcceso.Domain.Tenant.PartidasRecursosTenant;

public interface IPartidaRecursoTenantRepository
{

    void Add(PartidaRecursoTenant especialidad);
    void Update(PartidaRecursoTenant especialidad);
    void Delete(PartidaRecursoTenant especialidad);
    Task<PartidaRecursoTenant?> GetByIdAsync(PartidaRecursoTenantId especialidadId, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    
}