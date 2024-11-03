using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Root.Users.LogoutUser;

public class LogoutUserResponse
{
    public bool IsTenant { get; set; }
    public string? IdTenant { get; set; }


    public static LogoutUserResponse Create(string idTenant, bool isTenant)
    {
        return new LogoutUserResponse{
            IdTenant = idTenant,
            IsTenant = isTenant,
        };
    }

}
