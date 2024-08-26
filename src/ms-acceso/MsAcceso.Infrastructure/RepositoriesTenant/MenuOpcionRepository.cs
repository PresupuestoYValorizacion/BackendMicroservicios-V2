using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class MenuOpcionRepository : RepositoryTenant<MenuOpcion, MenuOpcionId>, IMenuOpcionRepository
{
    public MenuOpcionRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<MenuOpcion>> GetAllMenuOpcionsByMenuId(SistemaId sistemaId,CancellationToken cancellationToken)
    {
        return await DbContext.Set<MenuOpcion>().Where(m => m.MenusId == sistemaId && m.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

    public async Task<bool> MenuOpcionExists(OpcionId opcionId, SistemaId sistemaId, CancellationToken cancellationToken)
    {
        return await DbContext.Set<MenuOpcion>().AnyAsync(m => m.OpcionesId == opcionId && m.MenusId == sistemaId && m.Activo == new Activo(true) , cancellationToken);
    }
}