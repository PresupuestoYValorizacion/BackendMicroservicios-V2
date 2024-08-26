

namespace MsAcceso.Domain.Root.Licencias;

public interface ILicenciaRepository
{

    Task<Licencia?> GetByIdAsync(LicenciaId id, CancellationToken cancellationToken = default);

    void Add(Licencia user);

    void Update(Licencia user);
    
    void Delete(Licencia user);

    Task<List<Licencia>> GetAll(CancellationToken cancellationToken = default);
    

}