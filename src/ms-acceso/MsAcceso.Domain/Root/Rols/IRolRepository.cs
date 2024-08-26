
using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Domain.Root.Rols;

public interface IRolRepository
{

    Task<Rol?> GetByIdAsync(RolId id, CancellationToken cancellationToken = default);
    Task<Rol?> GetByLicenciaAsync(LicenciaId id, CancellationToken cancellationToken = default);

    void Add(Rol user);

    void Update(Rol user);
    
    void Delete(Rol user);

    

}