using Microsoft.AspNetCore.Identity;
using SkiNet.Core.Entities.Identity;

namespace SkiNet.Infrastructure.Identity;

public class AppIdentityDbContextSeed
{
  public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
  {
    if (!userManager.Users.Any())
    {
      var user = new AppUser
      {
        DisplayName = "Elijah",
        Email = "elijah@test.com",
        UserName = "elijah.doe",
        Address = new Address
        {
          FirstName = "Elijah",
          LastName = "Doe",
          Street = "1234 Main St",
          City = "New York",
          State = "NY",
          ZipCode = "90210"
        }
      };

      await userManager.CreateAsync(user, "Pa$$w0rd");
    }
  }
}