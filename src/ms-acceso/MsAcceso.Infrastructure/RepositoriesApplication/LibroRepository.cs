using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Root.Paginations;
using MsAcceso.Domain.Root.Libros;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class LibroRepository : RepositoryApplication<Libro, LibroId>, ILibroRepository, IPaginationLibroRepository
{

    public LibroRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<List<Libro>> GetAllLibro(CancellationToken cancellationToken)
    {
        return await DbContext.Set<Libro>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

    public async Task<bool> LibroExist(string nombreLibro, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Libro>().AnyAsync(x => x.Nombre == nombreLibro && x.Activo == new Activo(true), cancellationToken);
    }
}
