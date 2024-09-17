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
        SistemaId menuId,
        bool tieneUrl, 
        string url,
        int orden
        ): base( id )
    {
        OpcionesId = opcionId;
        MenusId = menuId;
        Url = url;
        TieneUrl = tieneUrl;
        Orden = orden;
    }
    
    public OpcionId? OpcionesId { get; set; }
    public SistemaId? MenusId{ get; set; }
    public bool TieneUrl { get; set; }
    public string? Url { get; set; }
    public int Orden { get; set; }
    public Opcion? Opcion{ get; set; }
    public Sistema? Menu{ get; set; }

    public static MenuOpcion Create(
        OpcionId opcionId,
        SistemaId sistemaId,
        bool tieneUrl,
        string url,
        int orden
    )
    {
        var menuOpcion = new MenuOpcion(MenuOpcionId.New(),opcionId,sistemaId, tieneUrl, url, orden);
        return menuOpcion;
    }

    public Result Update(OpcionId opcionId)
    {
        OpcionesId = (opcionId is not null) ? opcionId : OpcionesId;
        return Result.Success();
    }

    public Result Desactive()
    {
        var nuevoValorActivo = Activo!.Value;
        Activo = new Activo(!nuevoValorActivo);
        return Result.Success();
    }

}