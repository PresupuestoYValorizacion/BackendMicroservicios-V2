

namespace MsAcceso.Domain.Root.Licencias;

public interface ILicenciaRepository
{
    void Add(Licencia user);

    void Update(Licencia user);
    
    void Delete(Licencia user);

    Task<Licencia?> GetByIdAsync(LicenciaId licenciaId, CancellationToken cancellationToken);
    
    Task<bool> LicenciaExists(string licenciaNombre, CancellationToken cancellationToken = default);

    Task<List<Licencia>> GetAll(CancellationToken cancellationToken = default);

    Task<Licencia?> GetByNombre(string nombre, CancellationToken cancellationToken = default);
    
    Task<List<Licencia>> GetAllActive(CancellationToken cancellationToken = default);

}