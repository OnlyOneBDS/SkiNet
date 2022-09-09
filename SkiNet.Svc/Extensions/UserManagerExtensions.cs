using System.Security.Claims;
using SkiNet.Core.Entities.Identity;

namespace SkiNet.Svc.Extensions;

public static class UserManagerExtensions
{
  public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> input, ClaimsPrincipal user)
  {
    var email = user.FindFirstValue(ClaimTypes.Email);

    return await input.Users.Include(a => a.Address).SingleOrDefaultAsync(u => u.Email == email);
  }

  public static async Task<AppUser> FindByEmailFromClaimsPrincipal(this UserManager<AppUser> input, ClaimsPrincipal user)
  {
    var email = user.FindFirstValue(ClaimTypes.Email);

    return await input.Users.SingleOrDefaultAsync(u => u.Email == email);
  }
}