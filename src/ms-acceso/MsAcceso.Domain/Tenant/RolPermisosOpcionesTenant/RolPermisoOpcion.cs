using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.RolPermisos;
using MsAcceso.Domain.Tenant.RolPermisosTenant;

namespace MsAcceso.Domain.Tenant.RolPermisosOpcionesTenant;

public sealed class RolPermisoOpcionTenant : Entity<RolPermisoOpcionTenantId>
{
    private RolPermisoOpcionTenant(){}

    private RolPermisoOpcionTenant(
        RolPermisoOpcionTenantId id,
        RolPermisoTenantId rolPermisoId,
        string opcionId
        ): base( id )
    {
        RolPermisoId = rolPermisoId;
        OpcionId = opcionId;
    }
    
    public RolPermisoTenantId? RolPermisoId { get; set; }
    public string? OpcionId{ get; set; }
    public RolPermisoTenant? RolPermiso { get; set; }
    // public Opcion? Opcion{ get; set; }

    public static RolPermisoOpcionTenant Create(
        RolPermisoTenantId rolPermisoId,
        string opcionId
    )
    {
        var rolPermisoOpcion = new RolPermisoOpcionTenant(RolPermisoOpcionTenantId.New(),rolPermisoId, opcionId);

        return rolPermisoOpcion;
    }

    public Result Update(
        RolPermisoTenantId rolPermisoId,
        string opcionId
    )
    {
        RolPermisoId = rolPermisoId;
        OpcionId = opcionId;

        return Result.Success();
    }

}