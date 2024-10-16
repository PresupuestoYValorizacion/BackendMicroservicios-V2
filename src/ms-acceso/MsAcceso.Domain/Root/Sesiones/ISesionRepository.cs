
namespace MsAcceso.Domain.Root.Sesiones;

public interface ISesionRepository
{

    Task<Sesion?> GetByIdAsync(SesionId id, CancellationToken cancellationToken = default);
    Task<Sesion?> GetByUserId(string userId, CancellationToken cancellationToken = default);
    Task<Sesion?> GetSessionByTokenAsync(string token, CancellationToken cancellationToken = default);

    void Add(Sesion sesion);

    void Update(Sesion sesion);
    
    void Delete(Sesion sesion);

    

}