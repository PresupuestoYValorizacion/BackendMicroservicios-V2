using MsAcceso.Domain.Abstractions;
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

}