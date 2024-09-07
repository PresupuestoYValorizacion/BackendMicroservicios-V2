using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.RolPermisosOpciones;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Domain.Root.RolPermisos;

public sealed class RolPermiso : Entity<RolPermisoId>
{
    private RolPermiso(){}

    private RolPermiso(
        RolPermisoId id,
        RolId rolId,
        SistemaId menuId
        ): base( id )
    {
        RolId = rolId;
        MenuId = menuId;
    }
    
    public RolId? RolId { get; set; }
    public SistemaId? MenuId{ get; set; }
    public Rol? Rol {get; set; }
    public Sistema? Menu { get; set; }
    public List<RolPermisoOpcion>? RolPermisoOpcions { get; set; }

     public static RolPermiso Create(
        RolId rolId,
        SistemaId menuId
    )
    {
        var rolPermiso = new RolPermiso(RolPermisoId.New(),rolId, menuId);

        return rolPermiso;
    }

    public Result Update(
        RolId rolId,
        SistemaId menuId
    )
    {
        RolId = rolId;
        MenuId = menuId;

        return Result.Success();
    }
}