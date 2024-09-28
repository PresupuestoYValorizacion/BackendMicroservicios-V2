

using MsAcceso.Domain.Root.UsuarioLicencias;
using MsAcceso.Domain.Tenant.PersonasTenant;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Domain.Tenant.UsersTenant;
public class UserTenantDto
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    public PersonaTenantDto? Empresa { get; set; }
    public string? EmpresaId { get; set; }
    public string? RolId { get; set; }
    public RolTenantDto? Rol { get; set; }

}