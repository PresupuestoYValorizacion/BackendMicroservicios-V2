namespace MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
public interface ICarpetaPresupuestalTenantRepository
{
    void Add(CarpetaPresupuestalTenant carpetaPresupuestal);
    void Update(CarpetaPresupuestalTenant carpetaPresupuestal);
    void Delete(CarpetaPresupuestalTenant carpetaPresupuestal);
    Task<List<CarpetaPresupuestalTenant>> GetAllCarpetaPresupuestales(CancellationToken cancellationToken);
    Task<CarpetaPresupuestalTenant?> GetByIdAsync(CarpetaPresupuestalTenantId carpetaPresupuestalId, CancellationToken cancellationToken);
    Task<CarpetaPresupuestalTenant?> GetByNombreAsync(string nombre,int nivel,string? dependencia, CancellationToken cancellationToken);
    Task<bool> CarpetaPresupuestalExistsByName(string carpetaPresupuestalNombre, CancellationToken cancellationToken = default);
    Task<List<CarpetaPresupuestalTenant>> GetAllAsync(CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}
