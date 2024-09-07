
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Domain.Root.Rols;
public class RolDto
{
    public string? Id { get; set; }
    public string? TipoRolId { get; set; }
    public string? Nombre { get; set; }    
    public string? LicenciaId { get; set; }    
    public LicenciaDto? Licencia { get; set; }   
    public ParametroDto? TipoRol   { get; set; }

}