using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Paginations;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class ParametroRepository : RepositoryApplication<Parametro, ParametroId>, IParametroRepository, IPaginationParametrosRepository
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


     public async Task<List<Parametro>> GetAllParametrosBySubnivelToDelete(ParametroId Id, CancellationToken cancellationToken)
    {
        var dependentParametros = await DbContext.Set<Parametro>()
                                             .Where(x => x.Dependencia == Id)
                                             .ToListAsync(cancellationToken);

        var parametrosToProcess = new List<Parametro>(dependentParametros);

        while (parametrosToProcess.Count > 0)
        {
            var parametro = parametrosToProcess[0];
            parametrosToProcess.RemoveAt(0);

            var childSistemas = await LoadDependenciesToDeleteAsync(parametro, cancellationToken);

            foreach (var childSistema in childSistemas)
            {
                if (!dependentParametros.Any(s => s.Id == childSistema.Id))
                {
                    dependentParametros.Add(childSistema);
                    parametrosToProcess.Add(childSistema);
                }
            }
        }

        return dependentParametros!;
    }

    private async Task<List<Parametro>> LoadDependenciesToDeleteAsync(Parametro parametro, CancellationToken cancellationToken)
    {

        var childSistemas = await DbContext.Set<Parametro>()
                                       .Where(x => x.Dependencia == parametro.Id)
                                       .ToListAsync(cancellationToken);

        return childSistemas;

    }
}