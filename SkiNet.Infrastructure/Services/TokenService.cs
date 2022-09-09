using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SkiNet.Core.Entities.Identity;
using SkiNet.Core.Interfaces;

namespace SkiNet.Infrastructure.Services;

public class TokenService : ITokenService
{
  private readonly IConfiguration _config;
  private readonly SymmetricSecurityKey _key;

  public TokenService(IConfiguration config)
  {
    _config = config;
    _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
  }

  public string CreateToken(AppUser user)
  {
    var claims = new List<Claim>
    {
      new Claim(ClaimTypes.GivenName, user.DisplayName),
      new Claim(ClaimTypes.Email, user.Email),
    };

    var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Expires = DateTime.Now.AddDays(7),
      Issuer = _config["Token:Issuer"],
      SigningCredentials = creds,
      Subject = new ClaimsIdentity(claims)
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
  }
}