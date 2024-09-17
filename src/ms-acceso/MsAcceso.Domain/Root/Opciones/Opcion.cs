using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.RolPermisosOpciones;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.Opciones;

public sealed class Opcion : Entity<OpcionId>
{
    private Opcion(){}

    private Opcion(
        OpcionId id,
        string nombre,
        string icono,
        string tooltip
    ) : base(id)
    {
        Nombre = nombre;
        Icono = icono;
        Tooltip = tooltip;
    }

    public string? Nombre { get; private set; }
    public string? Icono { get; private set; }
    public string? Tooltip { get; private set; }
    public List<RolPermisoOpcion>? RolPermisoOpcions { get; set; }

    public static Opcion Create(
        string Nombre,
        string Icono,
        string Abreviatura
    )
    {
        var opcion = new Opcion(OpcionId.New(),Nombre,Icono,Abreviatura);
        return opcion;
    }

    public Result Update(
        string nombre,
        string icono,
        string tooltip
    )
    {
        Nombre = (nombre.Length > 0) ? nombre : Nombre;
        Icono   = icono;
        Tooltip =  tooltip;

        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}