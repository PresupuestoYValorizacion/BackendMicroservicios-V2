using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Root.Users.LoginUser;

public class LoginUserResponse
{
    public bool HasSessionActive { get; set; }
    public string? Token { get; set; }

    public UserDto? User { get; set; }

    public static LoginUserResponse Create(string token, UserDto user, bool hasSessionActive)
    {
        return new LoginUserResponse{
            Token = token,
            User = user,
            HasSessionActive = hasSessionActive
        };
    }

    public static LoginUserResponse HasSession( bool hasSessionActive)
    {
        return new LoginUserResponse{
            Token = "",
            HasSessionActive = hasSessionActive
        };
    }

}
