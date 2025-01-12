using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;

namespace MsAcceso.Domain.Tenant.PartidasTenant;
public interface IPartidaTenantRepository
{
    void Add(PartidaTenant partida);
    void Update(PartidaTenant partida);
    void Delete(PartidaTenant partida);

    Task<string?> GetLastCorrelativoAsync(PresupuestoEspecialidadTituloTenantId especialidadTituloId, CancellationToken cancellationToken = default);
    Task<string?> GetLastCorrelativoDependenciaAsync(int nivel, PartidaTenantId dependencia, CancellationToken cancellationToken = default);

    // Task<List<PartidaTenant>> GetAllPartidasBySubnivel(PartidaTenantId Id, CancellationToken cancellationToken);
    Task<PartidaTenant?> GetByIdAsync(PartidaTenantId partidaId, CancellationToken cancellationToken);
    Task<PartidaTenant?> GetByIdWithIncludesAsync(PartidaTenantId partidaId, CancellationToken cancellationToken);
    Task<bool> PartidaExistsByName(string partidaNombre,PartidaTenantId dependencia, PresupuestoEspecialidadTituloTenantId especialidadTituloId, CancellationToken cancellationToken = default);
    Task<List<PartidaTenant>> GetAllAsync(CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}
