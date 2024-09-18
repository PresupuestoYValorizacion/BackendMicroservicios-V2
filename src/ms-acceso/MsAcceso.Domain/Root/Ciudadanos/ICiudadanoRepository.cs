

namespace MsAcceso.Domain.Root.Ciudadanos;

public interface ICiudadanoRepository
{

    Task<List<Ciudadano>> GetAll(CancellationToken cancellationToken = default);

}