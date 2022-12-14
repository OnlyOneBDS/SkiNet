using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiNet.Core.Entities.OrderAggregate;

namespace SkiNet.Infrastructure.Data.Config;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.OwnsOne(o => o.ShippingAddress, a => { a.WithOwner(); });
    builder.Property(s => s.Status).HasConversion(o => o.ToString(), o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o));
    builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
  }
}