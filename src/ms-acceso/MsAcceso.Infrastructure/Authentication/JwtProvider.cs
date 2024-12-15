using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MsAcceso.Application.Abstractions.Authentication;
using MsAcceso.Domain.Root.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.UsersTenant;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Infrastructure.Authentication;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly IUserRepository _userRepository;
    // private readonly IUserTenantRepository _userTenantRepository;

    public JwtProvider(
        IOptions<JwtOptions> options, 
        IUserRepository userRepository
        // IUserTenantRepository userTenantRepository
        )
    {
        _options = options.Value;
        _userRepository = userRepository;
        // _userTenantRepository = userTenantRepository;
    }

    public async Task<string> Generate(User user)
    {
        var userFounded = await _userRepository.GetByIdUserIncludes(user.Id!);

        bool isAdmin = userFounded!.Rol!.TipoRolId!.Value == TipoRol.Administrador;
        bool isTenant = false;

        var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.Sub,user.Id!.Value.ToString()),
            new(CustomClaims.Email,user.Email!),
            new(CustomClaims.Rol,user.RolId!.Value.ToString()),
            new(CustomClaims.Tenant,user.Id!.Value.ToString()),
            new(CustomClaims.IsAdmin,isAdmin.ToString()),
            new(CustomClaims.IsTenant,isTenant.ToString()), // Hace referencia a si su usuario y rol esta en su propia bd
            new(CustomClaims.UserTenantRolId,string.Empty), 
            new(CustomClaims.UserId,user.Id!.Value.ToString())
        
        };

        var sigingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey!)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(3),
            sigingCredentials
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }

     public async Task<string> GenerateForTenant(UserTenant user, RolId userTenantRolId, string tenantId)
    {
        // var userFounded = await _userTenantRepository.GetByIdUserIncludes(user.Id!);

        bool isAdmin = false;
        bool isTenant = true;

        var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.Sub,user.Id!.Value.ToString()),
            new(CustomClaims.Email,user.Email!),
            new(CustomClaims.Rol,userTenantRolId.Value.ToString()),
            new(CustomClaims.Tenant,tenantId),
            new(CustomClaims.IsAdmin,isAdmin.ToString()),
            new(CustomClaims.IsTenant,isTenant.ToString()), // Hace referencia a si su usuario y rol esta en su propia bd
            new(CustomClaims.UserTenantRolId,user.RolId!.Value.ToString()), 
            new(CustomClaims.UserId,user.Id!.Value.ToString())

        };

        var sigingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey!)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(3),
            sigingCredentials
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }

    public DateTime GetExpirationTime(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        return jwtToken.ValidTo;
    }
}