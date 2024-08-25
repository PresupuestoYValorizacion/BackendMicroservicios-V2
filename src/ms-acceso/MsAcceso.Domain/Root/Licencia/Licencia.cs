using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.Licencia;


public sealed class Licencia : Entity<LicenciaId>
{
    private Licencia() { }

    private Licencia(
        LicenciaId id,
        string nombre
        ) : base(id)
    {
        Nombre = nombre;
    }

    public string? Nombre { get; private set; }



    public static Licencia Create(
        LicenciaId LicenciaId,
         string nombre
    )
    {
        var licencia = new Licencia(LicenciaId, nombre);

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