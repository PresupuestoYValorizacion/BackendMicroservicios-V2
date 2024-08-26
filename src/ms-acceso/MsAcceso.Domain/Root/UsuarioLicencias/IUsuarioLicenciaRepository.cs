

namespace MsAcceso.Domain.Root.UsuarioLicencias;

public interface IUsuarioLicenciaRepository
{

    Task<UsuarioLicencia?> GetByIdAsync(UsuarioLicenciaId id, CancellationToken cancellationToken = default);

    void Add(UsuarioLicencia user);

    void Update(UsuarioLicencia user);
    
    void Delete(UsuarioLicencia user);
    

}