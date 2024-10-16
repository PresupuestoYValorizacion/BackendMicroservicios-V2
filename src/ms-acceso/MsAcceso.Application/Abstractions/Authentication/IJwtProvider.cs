
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Abstractions.Authentication;

public interface IJwtProvider 
{
    Task<string> Generate(User user);

    DateTime GetExpirationTime(string token);
}