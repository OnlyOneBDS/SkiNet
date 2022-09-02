using Microsoft.EntityFrameworkCore;
using SkiNet.Infrastructure.Data;
using SkiNet.Svc.Extensions;
using SkiNet.Svc.Helpers;
using SkiNet.Svc.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddControllers();

builder.Services.AddDbContext<StoreContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApplicationServices();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddCors(options =>
{
  options.AddPolicy("CorsPolicy", policy =>
  {
    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
  });
});

// Configure the HTTP request pipeline.

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwaggerDocumentation();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();
//app.MapFallbackToController();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
  var context = services.GetRequiredService<StoreContext>();

  await context.Database.MigrateAsync();
  await StoreContextSeed.SeedAsync(context, loggerFactory);
}
catch (Exception ex)
{
  var logger = loggerFactory.CreateLogger<Program>();
  logger.LogError(ex, "An error occurred during migration");
}

await app.RunAsync();