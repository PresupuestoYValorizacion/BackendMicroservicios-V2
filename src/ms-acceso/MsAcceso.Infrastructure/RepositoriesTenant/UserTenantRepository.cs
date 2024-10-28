
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Tenant.UsersTenant;
using MsAcceso.Infrastructure.Service;
using MsAcceso.Application.Tenant.Paginations;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class UserTenantRepository : RepositoryTenant<UserTenant,UserTenantId>, IUserTenantRepository, IPaginationUsersTenantRepository
{
    public UserTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<UserTenant?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<UserTenant>()
                    .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<bool> IsUserExists(string email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<UserTenant>()
                    .AnyAsync(x => x.Email == email,cancellationToken);
    }

    public async Task<bool> ValidateIdUsuarioExists(Guid idUsuario, CancellationToken cancellationToken = default)
    {
       return await DbContext.Set<UserTenant>()
                    .AnyAsync(x => x.Id == new UserTenantId(idUsuario),cancellationToken);
    }

    public async Task<UserTenant?> GetByIdUserIncludes(
        UserTenantId Id,
        CancellationToken cancellationToken = default
    )
    {
        var user = await DbContext.Set<UserTenant>()
        // .Include(u => u.Empresa).ThenInclude(e => e!.TipoDocumento)
        // .Include(u => u.Empresa).ThenInclude(e => e!.Tipo)
        .Include(u => u.Persona).ThenInclude(e => e!.PersonaJuridica)
        .Include(u => u.Persona).ThenInclude(e => e!.PersonaNatural)
        .Include(u => u.Rol)
        // .Include(u => u.UsuarioLicencias) 
        .FirstOrDefaultAsync(x => x.Id == Id && x.Activo == new Activo(true), cancellationToken);

        // if (user != null && user.Rol!.TipoRolId == new ParametroId( TipoRol.Licencia))
        // {
        //     user.UsuarioLicencias = user.UsuarioLicencias!
        //     .Where(ul => ul.Activo == new Activo(true) && 
        //             ((ul.FechaFin > DateTime.Now) || (ul.FechaInicio == null && ul.FechaFin == null)))
        //     .OrderByDescending(ul => ul.FechaFin)  // Ordenar por FechaFin, nulls quedan al final
        //     .Take(1)  // Tomar solo la primera licencia válida
        //     .ToList();
        // }

        return user;
    }
}