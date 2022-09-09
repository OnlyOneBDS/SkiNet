using SkiNet.Core.Entities.Identity;

namespace SkiNet.Core.Interfaces;

public interface ITokenService
{
  string CreateToken(AppUser user);
}