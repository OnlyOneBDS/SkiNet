using SkiNet.Core.Interfaces;
using SkiNet.Infrastructure.Services;
using SkiNet.Svc.Errors;

namespace SkiNet.Svc.Extensions;

public static class ApplicationServicesExtensions
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddSingleton<IResponseCacheService, ResponseCacheService>();

    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<IBasketRepository, BasketRepository>();
    services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

    services.AddScoped<IOrderService, OrderService>();
    services.AddScoped<IPaymentService, PaymentService>();
    services.AddScoped<ITokenService, TokenService>();

    services.Configure<ApiBehaviorOptions>(options =>
    {
      options.InvalidModelStateResponseFactory = actionContext =>
      {
        var errors = actionContext.ModelState
                                  .Where(e => e.Value.Errors.Count > 0)
                                  .SelectMany(x => x.Value.Errors)
                                  .Select(x => x.ErrorMessage)
                                  .ToArray();

        var errorResponse = new ApiValidationErrorResponse
        {
          Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
      };
    });

    return services;
  }
}