using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkiNet.Core.Entities;
using SkiNet.Core.Entities.OrderAggregate;

namespace SkiNet.Infrastructure.Data;

public class StoreContext : DbContext
{
  public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

  public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<OrderItem> OrderItems { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<ProductBrand> ProductBrands { get; set; }
  public DbSet<ProductType> ProductTypes { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
    {
      foreach (var entityType in modelBuilder.Model.GetEntityTypes())
      {
        var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
        var dateTimeProperties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset));

        foreach (var property in properties)
        {
          modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
        }

        foreach (var property in dateTimeProperties)
        {
          modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(new DateTimeOffsetToBinaryConverter());
        }
      }
    }
  }
}