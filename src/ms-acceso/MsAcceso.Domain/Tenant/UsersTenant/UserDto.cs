

using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.UsuarioLicencias;

namespace MsAcceso.Domain.Tenant.UsersTenant;
public class UserDto
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    public PersonaDto? Empresa { get; set; }
    public string? EmpresaId { get; set; }
    public string? RolId { get; set; }
    public bool? IsAdmin { get; set; }
    public RolDto? Rol { get; set; }
    public UsuarioLicenciaDto? Licencia { get; set; }

}