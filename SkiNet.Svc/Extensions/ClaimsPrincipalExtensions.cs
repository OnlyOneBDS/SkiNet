using System.Security.Claims;

namespace SkiNet.Svc.Extensions;

public static class ClaimsPrincipalExtensions
{
  public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
  {
    return user.FindFirstValue(ClaimTypes.Email);
  }
}