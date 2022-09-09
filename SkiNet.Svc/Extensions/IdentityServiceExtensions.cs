using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SkiNet.Core.Entities.Identity;

namespace SkiNet.Svc.Extensions;

public static class IdentityServiceExtensions
{
  public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
  {
    var builder = services.AddIdentityCore<AppUser>();

    builder = new IdentityBuilder(builder.UserType, builder.Services);

    builder.AddEntityFrameworkStores<AppIdentityDbContext>();
    builder.AddSignInManager<SignInManager<AppUser>>();

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Token:Issuer"]
              };
            });

    return services;
  }
}