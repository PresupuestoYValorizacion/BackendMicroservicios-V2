using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.RolPermisosOpciones;
using MsAcceso.Domain.Tenant.RolPermisosOpcionesTenant;

// using MsAcceso.Domain.Root.Rols;
// using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Domain.Tenant.RolPermisosTenant;

public sealed class RolPermisoTenant : Entity<RolPermisoTenantId>
{
    private RolPermisoTenant(){}

    private RolPermisoTenant(
        RolPermisoTenantId id,
        RolTenantId rolId,
        string menuId
        ): base( id )
    {
        RolId = rolId;
        MenuId = menuId;
    }
    
    public RolTenantId? RolId { get; set; }
    public string? MenuId{ get; set; }
    public RolTenant? Rol {get; set; }
    // public Sistema? Menu { get; set; }
    public List<RolPermisoOpcionTenant>? RolPermisoOpcions { get; set; }

     public static RolPermisoTenant Create(
        RolTenantId rolId,
        string menuId
    )
    {
        var rolPermisoTenant = new RolPermisoTenant(RolPermisoTenantId.New(),rolId, menuId);

        return rolPermisoTenant;
    }

    public Result Update(
        RolTenantId rolId,
        string menuId
    )
    {
        RolId = rolId;
        MenuId = menuId;

        return Result.Success();
    }
}