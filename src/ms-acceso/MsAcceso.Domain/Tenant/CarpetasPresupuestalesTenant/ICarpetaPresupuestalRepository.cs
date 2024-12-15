namespace MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
public interface ICarpetaPresupuestalTenantRepository
{
    void Add(CarpetaPresupuestalTenant carpetaPresupuestal);
    void Update(CarpetaPresupuestalTenant carpetaPresupuestal);
    void Delete(CarpetaPresupuestalTenant carpetaPresupuestal);
    // Task<List<CarpetaPresupuestalTenant>> GetAllCarpetaPresupuestalsBySubnivel(CarpetaPresupuestalTenantId Id, CancellationToken cancellationToken);
    Task<CarpetaPresupuestalTenant?> GetByIdAsync(CarpetaPresupuestalTenantId carpetaPresupuestalId, CancellationToken cancellationToken);
    Task<bool> CarpetaPresupuestalExistsByName(string carpetaPresupuestalNombre, CancellationToken cancellationToken = default);
    Task<List<CarpetaPresupuestalTenant>> GetAllAsync(CancellationToken cancellationToken);
}
