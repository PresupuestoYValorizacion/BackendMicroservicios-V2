

using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Domain.Root.UsuarioLicencias;

public interface IUsuarioLicenciaRepository
{

    Task<UsuarioLicencia?> GetByIdAsync(UsuarioLicenciaId id, CancellationToken cancellationToken = default);

    void Add(UsuarioLicencia user);

    void Update(UsuarioLicencia user);
    
    void Delete(UsuarioLicencia user);

    Task<UsuarioLicencia?> GetByUserAsync(UserId id, CancellationToken cancellationToken = default);

    

}