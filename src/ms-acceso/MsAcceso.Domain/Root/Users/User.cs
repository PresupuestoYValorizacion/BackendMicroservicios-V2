
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.UsuarioLicencias;
using MsAcceso.Domain.Shared;


namespace MsAcceso.Domain.Root.Users;

public sealed class User : Entity<UserId>
{
    private User() { }

    private User(
        UserId id,
        string email,
        string username,
        string password,
        string connectionString,
        PersonaId empresaId,
        RolId rolId) : base(id)
    {
        Username = username;
        Email = email;
        Password = password;
        ConnectionString = connectionString;
        EmpresaId = empresaId;
        RolId = rolId;
    }

    public User(
        UserId id,
        string email,
        string username,
        string password,
        string connectionString,
        PersonaId empresaId,
        RolId rolId,
        List<UsuarioLicencia> usuarioLicencias
        ) : base(id)
    {
        Username = username;
        Email = email;
        Password = password;
        ConnectionString = connectionString;
        EmpresaId = empresaId;
        RolId = rolId;
        UsuarioLicencias = usuarioLicencias;
    }

    public string? Email { get; private set; }
    public string? Username { get; private set; }
    public string? Password { get; private set; }
    public string? ConnectionString { get; private set; }
    public PersonaId? EmpresaId { get; private set; }
    public RolId? RolId { get; private set; }
    public Persona? Empresa { get; private set; }
    public Rol? Rol { get; private set; }
    public List<UsuarioLicencia>? UsuarioLicencias { get; set; }

    public static User Create(
        UserId userId,
        string username,
        string email,
        string password,
        string connectionString,
        PersonaId empresaId,
        RolId rolId
    )
    {
        var user = new User(userId, email, username, password, connectionString, empresaId, rolId);

        return user;
    }

    public Result Update(string username, string email)
    {
        Username = username.Length > 0 ? username : Username;
        Email = email.Length > 0 ? email : Email;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }

    public void FiltrarLicenciasActivas()
    {
        UsuarioLicencias = UsuarioLicencias!
            .Where(ul => ul.Activo == new Activo(true) && ul.FechaFin > DateTime.Now)
            .OrderByDescending(ul => ul.FechaFin)
            .Take(1)
            .ToList();
    }
}