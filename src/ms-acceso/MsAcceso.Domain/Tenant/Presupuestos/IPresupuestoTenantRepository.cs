
namespace MsAcceso.Domain.Tenant.Presupuestos;

public interface IPresupuestoTenantRepository
{

    Task<Presupuesto?> GetByIdAsync(PresupuestoId id, CancellationToken cancellationToken = default);

    void Add(Presupuesto user);

    void Update(Presupuesto user);
    
    void Delete(Presupuesto user);

    Task<List<Presupuesto>> GetAll(CancellationToken cancellationToken = default);


}