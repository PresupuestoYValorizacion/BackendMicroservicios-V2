using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.RolPermisos;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.RolPermisosTenant;

namespace MsAcceso.Domain.Tenant.RolsTenant;

public sealed class RolTenant : Entity<RolTenantId>
{
    private RolTenant(){}

    private RolTenant(
        RolTenantId id,
        string nombre
        ): base(id)
    {
        Nombre = nombre;
    }
    

    public string? Nombre {get; private set;}
    public List<RolPermisoTenant>? RolPermisos {get; set;}

    public static RolTenant Create(
        string nombre
    )
    {
        var rol = new RolTenant(RolTenantId.New(),nombre);

        return rol;
    }

    public Result Update(
        string? nombre
    )
    {
        Nombre = nombre!.Length > 0 ? nombre : Nombre;

        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }


}