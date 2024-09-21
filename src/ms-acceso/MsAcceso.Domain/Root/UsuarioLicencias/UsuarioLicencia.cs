using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Parametros;
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
        ParametroId periodoLicenciaId
        ) : base(id)
    {
        UserId = userId;
        LicenciaId = licenciaId;
        PeriodoLicenciaId = periodoLicenciaId;
    }

    public UserId? UserId { get; private set; }
    public LicenciaId? LicenciaId { get; private set; }
    public ParametroId? PeriodoLicenciaId { get; private set; }
    public DateTime? FechaInicio { get; private set; }
    public DateTime? FechaFin { get; private set; }

    public User? User { get; private set; }
    public Licencia? Licencia { get; private set; }
    public Parametro? PeriodoLicencia { get; set;} 

    public static UsuarioLicencia Create(
        LicenciaId licenciaId,
        UserId userId,
        ParametroId periodoLicenciaId
    )
    {
        var usuarioLicencia = new UsuarioLicencia(UsuarioLicenciaId.New(), licenciaId, userId, periodoLicenciaId);

        return usuarioLicencia;
    }

    public Result Update(
        DateTime fechaInicio,
        DateTime fechaFin
    )
    {
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}