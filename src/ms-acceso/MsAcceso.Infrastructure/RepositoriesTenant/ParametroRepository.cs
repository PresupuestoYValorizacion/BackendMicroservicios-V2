using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Paginations;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class ParametroRepository : RepositoryTenant<Parametro, ParametroId>, IParametroRepository, IPaginationParametrosRepository
{
    public ParametroRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<int> GetLastParametroIdAsync(CancellationToken cancellationToken = default)
    {
        var lastParametro = await DbContext.Set<Parametro>()
                                        .OrderByDescending(p => p.Id)
                                        .FirstOrDefaultAsync(cancellationToken);
        return lastParametro!.Id!.Value;
    }

    public async Task<bool> ParametroExists(string parametroNombre,int nivel, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Parametro>()
                        .AnyAsync(x => x.Nombre == parametroNombre && x.Nivel == nivel && x.Activo==new Activo(true), cancellationToken);
    }

    public async Task<bool> ValorExists(string valor, int dependencia, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Parametro>()
                        .AnyAsync(x => x.Valor == valor && x.Dependencia == new ParametroId(dependencia) && x.Activo==new Activo(true), cancellationToken);
    }

    public async Task<List<Parametro>> GetRelatedEntitiesAsync(int parametroId, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Parametro>()
            .Where(p => p.Dependencia == new ParametroId(parametroId))
            .ToListAsync(cancellationToken);
    }
}