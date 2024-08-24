namespace MsAcceso.Domain.Root.Sistemas;

public interface ISistemaRepository
{
    Task<Sistema?> GetByIdAsync(SistemaId Id,CancellationToken cancellationToken);
    void Add(Sistema sistema);
    void Update(Sistema sistema);
    void Delete(Sistema sistema);
    Task<bool> SistemaExistsByName(string name, CancellationToken cancellationToken);
    Task<List<Sistema>> GetAllSistemas(CancellationToken cancellationToken);
    Task<List<Sistema>> GetAllSistemasBySubnivel(SistemaId Id, CancellationToken cancellationToken);

}