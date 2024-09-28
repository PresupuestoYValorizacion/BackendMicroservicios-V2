
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PersonasTenant;
using MsAcceso.Domain.Tenant.RolsTenant;


namespace MsAcceso.Domain.Tenant.UsersTenant;

public sealed class UserTenant : Entity<UserTenantId>
{
    private UserTenant() { }

    private UserTenant(
        UserTenantId id,
        string email,
        string username,
        string password,
        string connectionString,
        PersonaTenantId personaId,
        RolTenantId rolId) : base(id)
    {
        Username = username;
        Email = email;
        Password = password;
        ConnectionString = connectionString;
        PersonaId = personaId;
        RolId = rolId;
    }

    public string? Email { get; private set; }
    public string? Username { get; private set; }
    public string? Password { get; private set; }

    //TODO : ELIMINAR ESTO
    public string? ConnectionString { get; private set; }
    public PersonaTenantId? PersonaId { get; private set; }
    public RolTenantId? RolId { get; private set; }
    public PersonaTenant? Persona { get; private set; }

    public RolTenant? Rol { get; private set; }

    public static UserTenant Create(
        UserTenantId userId,
        string username,
        string email,
        string password,
        string connectionString,
        PersonaTenantId empresaId,
        RolTenantId rolId
    )
    {
        var user = new UserTenant(userId, email, username, password, connectionString, empresaId, rolId);

        return user;
    }

    public Result Update(
        string username, 
        string email, 
        string connectionString,
        RolTenantId rolId)
    {
        Username = username.Length > 0 ? username : Username;
        Email = email.Length > 0 ? email : Email;
        ConnectionString = connectionString;
        RolId = rolId;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }

}