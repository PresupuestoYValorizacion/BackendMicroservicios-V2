using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Ciudadanos;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.Pasaportes;

public sealed class Pasaporte : Entity<CiudadanoId>
{
    private Pasaporte() {}

    private Pasaporte(
        CiudadanoId id,
        string nroSerie
    ): base(id)
    {
        NroSerie = nroSerie;
    }

    public string? NroSerie {get; private set;}

    public static Pasaporte Create(
        CiudadanoId id,
        string NroSerie
    )
    {
        var pasaporte = new Pasaporte(id, NroSerie);
        return pasaporte;
    }
    public Result Update(
        string nroSerie
    )
    {
        NroSerie = (nroSerie.Length > 0) ? nroSerie : NroSerie;

        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}