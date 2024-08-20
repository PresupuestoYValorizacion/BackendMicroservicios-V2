using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Domain.Root.MenuOpciones;

public sealed class MenuOpcion : Entity<MenuOpcionId>
{
    private MenuOpcion(){}

    private MenuOpcion(
        MenuOpcionId id,
        OpcionId opcionId,
        SistemaId menuId
        ): base( id )
    {
        OpcionId = opcionId;
        MenuId = menuId;
    }
    
    public OpcionId? OpcionId { get; set; }
    public SistemaId? MenuId{ get; set; }

}