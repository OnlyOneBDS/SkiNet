using Microsoft.OpenApi.Models;

namespace SkiNet.Svc.Extensions;

public static class SwaggerServiceExtensions
{
  public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
  {
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    //services.AddEndpointsApiExplorer(); -- not sure if this is needed
    services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "SkiNet Svc", Version = "v1" });

      var securitySchema = new OpenApiSecurityScheme
      {
        Description = "JWT Auth Bearer Scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Scheme = "bearer",
        Type = SecuritySchemeType.Http,
        Reference = new OpenApiReference
        {
          Id = "Bearer",
          Type = ReferenceType.SecurityScheme,
        },
      };

      c.AddSecurityDefinition("Bearer", securitySchema);

      var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };

      c.AddSecurityRequirement(securityRequirement);
    });

    return services;
  }

  public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
  {
    app.UseSwagger();
    app.UseSwaggerUI();

    return app;
  }
}