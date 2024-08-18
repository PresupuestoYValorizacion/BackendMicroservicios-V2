

using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Users.LoginUser;

public class LoginUserResponse
{
    public string? Token { get; set; }

    public UserDto? User { get; set; }

    public static LoginUserResponse Create(string token, UserDto user)
    {
        return new LoginUserResponse{
            Token = token,
            User = user
        };
    }
}
