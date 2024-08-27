
using MsAcceso.Domain.Tenant.Pruebas;

namespace MsAcceso.Domain.Tenant.Presupuestos;

public interface IPruebaTenantRepository
{

    Task<Prueba?> GetByIdAsync(PruebaId id, CancellationToken cancellationToken = default);

    void Add(Prueba user);

    void Update(Prueba user);
    
    void Delete(Prueba user);

    Task<List<Prueba>> GetAll(CancellationToken cancellationToken = default);


}