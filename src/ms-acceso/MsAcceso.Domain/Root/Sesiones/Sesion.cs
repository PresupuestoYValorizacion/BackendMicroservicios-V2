
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Shared;


namespace MsAcceso.Domain.Root.Sesiones;

public sealed class Sesion : Entity<SesionId>
{
    private Sesion() { }

    private Sesion(
        SesionId id,
        string userId,
        string jwtToken,
        DateTime lastActivity) : base(id)
    {
        UserId = userId;
        JwtToken = jwtToken;
        LastActivity = lastActivity;
    }

    

    public string? UserId { get; private set; }
    public string? JwtToken { get; private set; }
    public DateTime? LastActivity { get; private set; }
    public static Sesion Create(
        string userId,
        string jwtToken
    )
    {
        var sesion = new Sesion(SesionId.New(),userId, jwtToken, DateTime.UtcNow);

        return sesion;
    }

    public Result Update(
        string jwtToken
        )
    {
        // Username = username.Length > 0 ? username : Username;
        // Email = email.Length > 0 ? email : Email;
        // ConnectionString = connectionString;
        // RolId = rolId;
        return Result.Success();
    }

    public Result UpdateLastActivity(
        DateTime? lastActivity
        )
    {
        LastActivity = lastActivity;
        
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }

}