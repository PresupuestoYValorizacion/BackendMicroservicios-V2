
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Domain.Root.Rols;

public interface IRolRepository
{

    Task<Rol?> GetByIdAsync(RolId id, CancellationToken cancellationToken = default);
    Task<Rol?> GetByLicenciaAsync(LicenciaId id, CancellationToken cancellationToken = default);
    Task<Rol?> GetRolByParametroAndLicencia(ParametroId parametroId,LicenciaId id, CancellationToken cancellationToken = default);
    Task<List<Rol>> GetRolesByTipoAsync(ParametroId TipoId, CancellationToken cancellationToken = default);
    Task<bool> GetByNombreAsync(string nombre, CancellationToken cancellationToken = default);

    void Add(Rol user);

    void Update(Rol user);
    
    void Delete(Rol user);

    

}