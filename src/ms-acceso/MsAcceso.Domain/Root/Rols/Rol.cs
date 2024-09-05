using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.RolPermisos;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.Rols;

public sealed class Rol : Entity<RolId>
{
    private Rol(){}

    private Rol(
        RolId id,
        string nombre,
        ParametroId tipoRolId,
        LicenciaId? licenciaId
        ): base(id)
    {
        Nombre = nombre;
        TipoRolId = tipoRolId;
        LicenciaId = licenciaId;
    }
    

    public string? Nombre {get; private set;}
    public ParametroId? TipoRolId {get; private set;}
    public LicenciaId? LicenciaId {get; private set;}
    public Parametro? TipoRol {get; private set;}
    public Licencia? Licencia {get; private set;}
    public List<RolPermiso>? RolPermisos {get; set;}

    public static Rol Create(
        string nombre,
        ParametroId tipoRolId,
        LicenciaId? licenciaId
    )
    {
        var rol = new Rol(RolId.New(), nombre,tipoRolId, licenciaId);

        return rol;
    }

    public Result Update(
        string? nombre,
        ParametroId? tipoRolId,
        LicenciaId? licenciaId
    )
    {
        Nombre = nombre!.Length > 0 ? nombre : Nombre;
        TipoRolId = tipoRolId;
        LicenciaId = licenciaId;

        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }


}