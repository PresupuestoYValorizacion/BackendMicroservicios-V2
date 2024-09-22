using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MsAcceso.Application.Abstractions.Authentication;
using MsAcceso.Domain.Root.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MsAcceso.Infrastructure.Authentication;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly IUserRepository _userRepository;

    public JwtProvider(IOptions<JwtOptions> options, IUserRepository userRepository)
    {
        _options = options.Value;
        _userRepository = userRepository;
    }

    public  async Task<string> Generate(User user)
    {
        var userFounded = await _userRepository.GetByIdAsync(user.Id!);

        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub,user.Id!.Value.ToString()),
            new Claim(CustomClaims.Email,user.Email!),
            new Claim(CustomClaims.Rol,user.RolId!.Value.ToString()),
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
}