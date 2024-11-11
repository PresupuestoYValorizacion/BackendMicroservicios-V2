using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Shared;
//using MsAcceso.Application.Root.Paginations;
using MsAcceso.Domain.Tenant.Especialidades;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class EspecialidadRepository : RepositoryApplication<Especialidad, EspecialidadId>, IEspecialidadRepository//, IPaginationEspecialidadRepository
{

    public EspecialidadRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<List<Especialidad>> GetAllEspecialidad(CancellationToken cancellationToken)
    {
        return await DbContext.Set<Especialidad>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

    public async Task<bool> EspecialidadExist(string nombreEspecialidad, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Especialidad>().AnyAsync(x => x.Nombre == nombreEspecialidad && x.Activo == new Activo(true), cancellationToken);
    }
}