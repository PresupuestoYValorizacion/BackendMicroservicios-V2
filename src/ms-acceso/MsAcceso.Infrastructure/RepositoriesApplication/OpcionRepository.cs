using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Root.Paginations;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class OpcionRepository : RepositoryApplication<Opcion, OpcionId>, IOpcionRepository, IPaginationOpcionRepository
{

    public OpcionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<List<Opcion>> GetAllOpcion(CancellationToken cancellationToken)
    {
        return await DbContext.Set<Opcion>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

    public async Task<bool> OpcionExist(string nombreOpcion, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Opcion>().AnyAsync(x => x.Nombre == nombreOpcion && x.Activo == new Activo(true), cancellationToken);
    }
}