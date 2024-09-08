using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Domain.Root.Sistemas;

public interface ISistemaRepository
{
    Task<Sistema?> GetByIdAsync(SistemaId Id,CancellationToken cancellationToken);
    Task<Sistema?> GetByUrlAsync(string url,CancellationToken cancellationToken);
    void Add(Sistema sistema);
    void Update(Sistema sistema);
    void Delete(Sistema sistema);
    Task<bool> SistemaExistsByName(string name, CancellationToken cancellationToken);
    Task<bool> SistemaExistsByUrl(string url, CancellationToken cancellationToken);
    Task<List<Sistema>> GetAllSistemas(CancellationToken cancellationToken);
    Task<List<Sistema>> GetAllSistemasBySubnivel(SistemaId Id, CancellationToken cancellationToken);
    Task<List<Sistema>> GetAllSistemasByRol(RolId Id, CancellationToken cancellationToken);
    Task<Sistema?> SistemaGetByIdAsync(SistemaId Id,CancellationToken cancellationToken);
    Task<Sistema?> GetSistemaByIdAndRol(RolId rolId,SistemaId sistemaId, CancellationToken cancellationToken);



}