using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Tenant.Especialidades;

public sealed class Especialidad : Entity<EspecialidadId>
{
    private Especialidad(){}

    private Especialidad(
        EspecialidadId id,
        string nombre
    ) : base(id)
    {
        Nombre = nombre;
    }

    public string? Nombre { get; private set; }

    public static Especialidad Create(
        string Nombre
    )
    {
        var opcion = new Especialidad(EspecialidadId.New(), Nombre);
        return opcion;
    }

    public Result Update(
        string nombre
    )
    {
        Nombre = (nombre.Length > 0) ? nombre : Nombre;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}