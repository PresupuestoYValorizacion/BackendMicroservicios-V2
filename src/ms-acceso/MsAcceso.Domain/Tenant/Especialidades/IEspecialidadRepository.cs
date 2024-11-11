namespace MsAcceso.Domain.Tenant.Especialidades;

public interface IEspecialidadRepository
{

    void Add(Especialidad especialidad);
    void Update(Especialidad especialidad);
    void Delete(Especialidad especialidad);
    Task<Especialidad?> GetByIdAsync(EspecialidadId especialidadId, CancellationToken cancellationToken = default);
    Task<bool> EspecialidadExist(string nombreEspecialidad, CancellationToken cancellationToken = default);
    Task<List<Especialidad>> GetAllEspecialidad(CancellationToken cancellationToken);
    
}