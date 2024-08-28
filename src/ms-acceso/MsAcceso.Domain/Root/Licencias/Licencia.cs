using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.Licencias;


public sealed class Licencia : Entity<LicenciaId>
{
    private Licencia() { }

    private Licencia(
        LicenciaId id,
        string nombre,
        bool permiteCrearUsuarios
        ) : base(id)
    {
        PermiteCrearUsuarios = permiteCrearUsuarios;
        Nombre = nombre;
    }

    public string? Nombre { get; private set; }

    public bool PermiteCrearUsuarios { get; private set; }

    public static Licencia Create(
        LicenciaId LicenciaId,
        string nombre,
        bool permiteCrearUsuarios

    )
    {
        var licencia = new Licencia(LicenciaId, nombre,permiteCrearUsuarios);

        return licencia;
    }

    public Result Update(
        string nombre)
    {
        Nombre = nombre;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}

