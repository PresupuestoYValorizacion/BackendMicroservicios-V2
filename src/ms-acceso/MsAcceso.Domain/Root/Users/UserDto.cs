

using MsAcceso.Domain.Root.Personas;

namespace MsAcceso.Domain.Root.Users;
public class UserDto
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    public PersonaDto? Empresa { get; set; }
    public string? EmpresaId { get; set; }

}