
namespace MsAcceso.Domain.Root.Parametros;

public interface IParametroRepository
{
    void Add(Parametro parametro);
    void Update(Parametro parametro);
    void Delete(Parametro parametro);
    Task<Parametro?> GetByIdAsync(ParametroId parametroId, CancellationToken cancellationToken);
    Task<bool> ParametroExists(string parametroNombre,int nivel, CancellationToken cancellationToken = default);
    Task<bool> ValorExists(string valor,int dependencia, CancellationToken cancellationToken = default);
    Task<int> GetLastParametroIdAsync(CancellationToken cancellationToken = default);
    Task<List<Parametro>> GetRelatedEntitiesAsync(int parametroId, CancellationToken cancellationToken);
}