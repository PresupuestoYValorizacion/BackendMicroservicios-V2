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

}