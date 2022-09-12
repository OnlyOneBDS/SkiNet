using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiNet.Core.Entities.OrderAggregate;

namespace SkiNet.Infrastructure.Data.Config;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
  public void Configure(EntityTypeBuilder<OrderItem> builder)
  {
    builder.OwnsOne(i => i.ItemOrdered, io => { io.WithOwner(); });
    builder.Property(i => i.Price).HasPrecision(18, 2);
  }
}