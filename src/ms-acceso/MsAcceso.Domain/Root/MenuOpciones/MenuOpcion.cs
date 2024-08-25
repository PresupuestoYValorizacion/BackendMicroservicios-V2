using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;

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

    public static MenuOpcion Create(
        OpcionId opcionId,
        SistemaId sistemaId
    )
    {
        var menuOpcion = new MenuOpcion(MenuOpcionId.New(),opcionId,sistemaId);
        return menuOpcion;
    }

    public Result Update(OpcionId opcionId)
    {
        OpcionId = (opcionId is not null) ? opcionId : OpcionId;
        return Result.Success();
    }

    public Result Desactive()
    {
        var nuevoValorActivo = Activo!.Value;
        Activo = new Activo(!nuevoValorActivo);
        return Result.Success();
    }

}