namespace MsAcceso.Domain.Tenant.PartidasTenant;
public interface IPartidaTenantRepository
{
    void Add(PartidaTenant partida);
    void Update(PartidaTenant partida);
    void Delete(PartidaTenant partida);
    // Task<List<PartidaTenant>> GetAllPartidasBySubnivel(PartidaTenantId Id, CancellationToken cancellationToken);
    Task<PartidaTenant?> GetByIdAsync(PartidaTenantId partidaId, CancellationToken cancellationToken);
    Task<bool> PartidaExistsByName(string partidaNombre, CancellationToken cancellationToken = default);
    Task<List<PartidaTenant>> GetAllAsync(CancellationToken cancellationToken);
}
