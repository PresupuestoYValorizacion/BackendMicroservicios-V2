using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.RolPermisos;

namespace MsAcceso.Domain.Root.RolPermisosOpciones;

public sealed class RolPermisoOpcion : Entity<RolPermisoOpcionId>
{
    private RolPermisoOpcion(){}

    private RolPermisoOpcion(
        RolPermisoOpcionId id,
        RolPermisoId rolPermisoId,
        OpcionId opcionId
        ): base( id )
    {
        RolPermisoId = rolPermisoId;
        OpcionId = opcionId;
    }
    
    public RolPermisoId? RolPermisoId { get; set; }
    public OpcionId? OpcionId{ get; set; }
    public RolPermiso? RolPermiso { get; set; }
    public Opcion? Opcion{ get; set; }

    public static RolPermisoOpcion Create(
        RolPermisoId rolPermisoId,
        OpcionId opcionId
    )
    {
        var rolPermisoOpcion = new RolPermisoOpcion(RolPermisoOpcionId.New(),rolPermisoId, opcionId);

        return rolPermisoOpcion;
    }

    public Result Update(
        RolPermisoId rolPermisoId,
        OpcionId opcionId
    )
    {
        RolPermisoId = rolPermisoId;
        OpcionId = opcionId;

        return Result.Success();
    }

}