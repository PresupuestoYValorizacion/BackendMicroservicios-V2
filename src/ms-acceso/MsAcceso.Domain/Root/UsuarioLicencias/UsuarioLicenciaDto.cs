


namespace MsAcceso.Domain.Root.UsuarioLicencias;
public class UsuarioLicenciaDto
{
    public string? LicenciaId { get; set; }
    public string? UserId { get; set; }
    public int? PeriodoLicenciaId {get; set;}
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }

}