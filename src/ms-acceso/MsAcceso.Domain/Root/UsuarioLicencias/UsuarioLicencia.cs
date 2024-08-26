using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.UsuarioLicencias;


public sealed class UsuarioLicencia : Entity<UsuarioLicenciaId>
{
    private UsuarioLicencia() { }

    private UsuarioLicencia(
        UsuarioLicenciaId id,
        LicenciaId licenciaId,
        UserId userId,
        DateTime fechaInicio,
        DateTime fechaFin
        ) : base(id)
    {
        UserId = userId;
        LicenciaId = licenciaId;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;

    }

    public UserId? UserId { get; private set; }
    public LicenciaId? LicenciaId { get; private set; }
    public DateTime? FechaInicio { get; private set; }
    public DateTime? FechaFin { get; private set; }

    public User? User { get; private set; }
    public Licencia? Licencia { get; private set; }


    public static UsuarioLicencia Create(
        LicenciaId licenciaId,
        UserId userId,
        DateTime fechaInicio,
        DateTime fechaFin
    )
    {
        var usuarioLicencia = new UsuarioLicencia(UsuarioLicenciaId.New(), licenciaId, userId, fechaInicio, fechaFin);

        return usuarioLicencia;
    }

    // public Result Update(
    //     string nombre)
    // {
    //     Nombre = nombre;
    //     return Result.Success();
    // }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}