using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Domain.Root.MenuOpciones;

public interface IMenuOpcionRepository
{
    void Add(MenuOpcion menuOpcion);
    void Update(MenuOpcion menuOpcion);
    void Delete(MenuOpcion menuOpcion);
    Task<MenuOpcion?> GetByIdAsync(MenuOpcionId menuOpcionId, CancellationToken cancellationToken);
    Task<bool> MenuOpcionExists(OpcionId opcionId, SistemaId sistemaId, CancellationToken cancellationToken);
    Task<MenuOpcion?> GetMenuOpcion(OpcionId opcionId, SistemaId sistemaId, CancellationToken cancellationToken);

    Task<List<MenuOpcion>> GetAllMenuOpcionsByMenuId(SistemaId sistemaId,CancellationToken cancellationToken);
}