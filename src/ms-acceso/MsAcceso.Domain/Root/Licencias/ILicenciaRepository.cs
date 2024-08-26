
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Domain.Root.Rols;

public interface ILicenciaRepository
{

    Task<Licencia?> GetByIdAsync(LicenciaId id, CancellationToken cancellationToken = default);

    void Add(Licencia user);

    void Update(Licencia user);
    
    void Delete(Licencia user);

    Task<List<Licencia>> GetAll(CancellationToken cancellationToken = default);
    

}