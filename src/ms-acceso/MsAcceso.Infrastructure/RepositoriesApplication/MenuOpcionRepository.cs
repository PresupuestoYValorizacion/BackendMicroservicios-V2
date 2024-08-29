using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class MenuOpcionRepository : RepositoryApplication<MenuOpcion, MenuOpcionId>, IMenuOpcionRepository
{
    public MenuOpcionRepository(ApplicationDbContext dbContext) : base(dbContext)
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

    public async Task<MenuOpcion?> GetMenuOpcion(OpcionId opcionId, SistemaId sistemaId, CancellationToken cancellationToken)
    {
        return await DbContext.Set<MenuOpcion>().FirstOrDefaultAsync(m => m.OpcionesId == opcionId && m.MenusId == sistemaId && m.Activo == new Activo(true), cancellationToken);
    }
}