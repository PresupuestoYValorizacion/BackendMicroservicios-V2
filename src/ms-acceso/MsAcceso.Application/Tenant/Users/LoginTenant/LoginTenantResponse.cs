using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Tenant.UsersTenant;

namespace MsAcceso.Application.Tenant.Users.LoginTenant;

public class LoginTenantResponse
{
    public bool HasSessionActive { get; set; }
    public string? Token { get; set; }

    public UserTenantDto? User { get; set; }

    public static LoginTenantResponse Create(string token, UserTenantDto user, bool hasSessionActive)
    {
        return new LoginTenantResponse{
            Token = token,
            User = user,
            HasSessionActive = hasSessionActive
        };
    }

    public static LoginTenantResponse HasSession( bool hasSessionActive)
    {
        return new LoginTenantResponse{
            Token = "",
            HasSessionActive = hasSessionActive
        };
    }

}
