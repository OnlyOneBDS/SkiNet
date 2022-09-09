using SkiNet.Core.Entities.Identity;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddControllers();

builder.Services.AddDbContext<StoreContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
{
  var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
  return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddApplicationServices();
builder.Services.AddIdentityServices(builder.Configuration);

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

app.UseAuthentication();
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

  var userManager = services.GetRequiredService<UserManager<AppUser>>();
  var identityContext = services.GetRequiredService<AppIdentityDbContext>();

  await identityContext.Database.MigrateAsync();
  await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
}
catch (Exception ex)
{
  var logger = loggerFactory.CreateLogger<Program>();
  logger.LogError(ex, "An error occurred during migration");
}

await app.RunAsync();