namespace MsAcceso.Domain.Tenant.RecursosTenant;

public interface IRecursoTenantRepository
{

    void Add(RecursoTenant recurso);
    void Update(RecursoTenant recurso);
    void Delete(RecursoTenant recurso);
    Task<RecursoTenant?> GetByIdAsync(RecursoTenantId recursoId, CancellationToken cancellationToken = default);
    Task<bool> RecursoExist(string nombreRecurso, CancellationToken cancellationToken = default);
    Task<List<RecursoTenant>> GetAllAsync(CancellationToken cancellationToken);
    
}