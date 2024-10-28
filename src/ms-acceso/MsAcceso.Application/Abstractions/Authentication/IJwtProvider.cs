
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Tenant.UsersTenant;

namespace MsAcceso.Application.Abstractions.Authentication;

public interface IJwtProvider 
{
    Task<string> Generate(User user);
    Task<string> GenerateForTenant(UserTenant user);

    DateTime GetExpirationTime(string token);
}