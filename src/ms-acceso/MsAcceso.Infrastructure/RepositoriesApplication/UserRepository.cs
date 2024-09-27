
using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Root.Paginations;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class UserRepository : RepositoryApplication<User, UserId>, IUserRepository, IPaginationUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<User>()
                    .Include(u => u.Rol)
                    .Include(u => u.UsuarioLicencias) 
                    .FirstOrDefaultAsync(x => x.Email == email && x.Activo == new Activo(true), cancellationToken);
    }

    public async Task<bool> IsUserExists(string email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<User>()
                    .AnyAsync(x => x.Email == email && x.Activo == new Activo(true), cancellationToken);
    }

    public async Task<User?> GetByIdUserIncludes(
        UserId Id,
        CancellationToken cancellationToken = default
    )
    {
        var user = await DbContext.Set<User>()
        .Include(u => u.Empresa).ThenInclude(e => e!.TipoDocumento)
        .Include(u => u.Empresa).ThenInclude(e => e!.Tipo)
        .Include(u => u.Empresa).ThenInclude(e => e!.PersonaJuridica)
        .Include(u => u.Empresa).ThenInclude(e => e!.PersonaNatural)
        .Include(u => u.Rol)
        .Include(u => u.UsuarioLicencias) 
        .FirstOrDefaultAsync(x => x.Id == Id && x.Activo == new Activo(true), cancellationToken);

        if (user != null && user.UsuarioLicencias != null)
        {
            user.UsuarioLicencias = user.UsuarioLicencias!
            .Where(ul => ul.Activo == new Activo(true) && 
                    ((ul.FechaFin > DateTime.Now) || (ul.FechaInicio == null && ul.FechaFin == null)))
            .OrderByDescending(ul => ul.FechaFin)  // Ordenar por FechaFin, nulls quedan al final
            .Take(1)  // Tomar solo la primera licencia válida
            .ToList();
        }

        return user;
    }

    public async Task<bool> ValidateIdUsuarioExists(Guid idUsuario, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<User>()
                     .AnyAsync(x => x.Id == new UserId(idUsuario) && x.Activo == new Activo(true), cancellationToken);
    }
}